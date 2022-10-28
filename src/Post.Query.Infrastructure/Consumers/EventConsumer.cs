using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Post.Query.Infrastructure.Converters;
using Post.Query.Infrastructure.Handlers;
using System.Text.Json;

namespace Post.Query.Infrastructure.Consumers;
public class EventConsumer : IEventConsumer {
    private readonly ConsumerConfig config;
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<EventConsumer> logger;

    public EventConsumer(IServiceProvider serviceProvider, IOptions<ConsumerConfig> config, ILogger<EventConsumer> logger) {
        this.config = config.Value;
        this.serviceProvider = serviceProvider;
        this.logger = logger;
    }
    public async Task Consume(string topic, CancellationToken stoppingToken) {
        using (var consumer = new ConsumerBuilder<string, string>(config)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(Deserializers.Utf8)
                .Build()) {

            consumer.Subscribe(topic);

            while (true) {
                var consumerResult = consumer.Consume(stoppingToken);
                if (consumerResult?.Message.Value == null) continue;

                logger.LogInformation("A message has been received");
                var @event = JsonSerializer.Deserialize<BaseEvent>(consumerResult.Message.Value,
                    new JsonSerializerOptions
                    {
                        Converters = { new EventJsonConverter() }
                    });

                using (IServiceScope scope = serviceProvider.CreateScope()) {
                    var eventHandler = scope.ServiceProvider.GetRequiredService<IEventHandler>();
                    var handleMethod = eventHandler.GetType().GetMethod("On", new Type[] { @event.GetType() });

                    if (handleMethod == null)
                        throw new ArgumentNullException(nameof(handleMethod), "Could not find event handler method!");

                    var awaitable = (Task)handleMethod.Invoke(eventHandler, new object[] { @event });
                    await awaitable;
                }

                consumer.Commit(consumerResult);
            }
            consumer.Close();
        }
    }
}