using CQRS.Core.Consumers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Post.Query.Infrastructure.Consumers;
public class ConsumerBackgroundService : BackgroundService {
    private readonly IEventConsumer eventConsumer;
    private readonly ILogger<ConsumerBackgroundService> logger;

    public ConsumerBackgroundService(IEventConsumer eventConsumer, ILogger<ConsumerBackgroundService> logger) {
        this.eventConsumer = eventConsumer;
        this.logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken) {
        logger.LogInformation("Event consumer service running");
        var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
        await Task.Run(async () => await eventConsumer.Consume(topic, stoppingToken), stoppingToken);
    }
}