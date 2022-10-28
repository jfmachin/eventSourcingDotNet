using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Infrastructure.Stores;
public class EventStore : IEventStore {
    private readonly IEventStoreRepository eventStoreRepository;
    private readonly IEventProducer eventProducer;

    public EventStore(IEventStoreRepository eventStoreRepository, IEventProducer eventProducer) {
        this.eventStoreRepository = eventStoreRepository;
        this.eventProducer = eventProducer;
    }

    public async Task<List<Guid>> GetAggregateIdsAsync() {
        var eventStream = await eventStoreRepository.FindAllAsync();
        if (eventStream == null || !eventStream.Any())
            throw new ArgumentNullException(nameof(eventStream), "Could not retrieve event stream from the event store!");
        return eventStream.Select(x => x.AggregateIdentifier).Distinct().ToList();
    }

    public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId) {
        var eventStream = await eventStoreRepository.CollectEventsByAggregateId(aggregateId);
        if (eventStream == null || !eventStream.Any())
            throw new AggregateNotFoundException("Incorrect aggregate id");

        return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
    }

    public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion) {
        var eventStream = await eventStoreRepository.CollectEventsByAggregateId(aggregateId);

        if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            throw new ConcurrencyException();

        var version = expectedVersion;

        foreach (var @event in events) {
            version++;
            @event.Version = version;
            var eventType = @event.GetType().Name;
            var eventModel = new EventModel { 
                TimeStamp = DateTime.Now,
                AggregateIdentifier = aggregateId,
                AggregateType = nameof(PostAggregate),
                Version = version,
                EventType = eventType,
                EventData = @event
            };

            await eventStoreRepository.SaveAsync(eventModel);
            var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
            await eventProducer.ProduceAsync(topic, @event);
        }
    }
}

