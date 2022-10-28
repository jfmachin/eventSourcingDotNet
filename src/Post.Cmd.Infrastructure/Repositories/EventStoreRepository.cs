using CQRS.Core.Domain;
using CQRS.Core.Events;
using MongoDB.Driver;
using Post.Cmd.Infrastructure.Data;

namespace Post.Cmd.Infrastructure.Repositories;

public class EventStoreRepository : IEventStoreRepository {
    private readonly IEventDbContext eventCollection;

    public EventStoreRepository(IEventDbContext eventCollection) {
        this.eventCollection = eventCollection;
    }

    public async Task<List<EventModel>> CollectEventsByAggregateId(Guid aggregateId) {
        return await eventCollection.Events.Find(x => x.AggregateIdentifier == aggregateId)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task<List<EventModel>> FindAllAsync() {
        return await eventCollection.Events.Find(_ => true).ToListAsync().ConfigureAwait(false);
    }

    public async Task SaveAsync(EventModel @event) {
        await eventCollection.Events.InsertOneAsync(@event);
    }
}