using Confluent.Kafka;
using CQRS.Core.Events;
using Microsoft.Extensions.Logging;
using CQRS.Core.Producers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Post.Cmd.Infrastructure.Producers;
public class EventProducer : IEventProducer {
    private readonly ProducerConfig config;
    private readonly ILogger<EventProducer> logger;

    public EventProducer(IOptions<ProducerConfig> config, ILogger<EventProducer> logger) {
        this.config = config.Value;
        this.logger = logger;
    }
    public async Task ProduceAsync<T>(string topic, T @event) where T : BaseEvent {
        var producer = new ProducerBuilder<string, string>(config)
            .SetKeySerializer(Serializers.Utf8)
            .SetValueSerializer(Serializers.Utf8)
            .Build();

        var msg = new Message<string, string> { 
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(@event, @event.GetType())
        };

        var delivery = await producer.ProduceAsync(topic, msg);
        logger.LogInformation("A message has been sent");
        if (delivery.Status == PersistenceStatus.NotPersisted)
            throw new Exception($"Could not produce {@event.GetType().Name} message to topic - {topic} due to the following reason: {delivery.Message}");
    }
}