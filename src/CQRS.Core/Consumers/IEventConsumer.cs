namespace CQRS.Core.Consumers;
public interface IEventConsumer {
    Task Consume(string topic, CancellationToken stoppingToken);
}