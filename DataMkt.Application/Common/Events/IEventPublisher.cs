namespace DataMkt.Application.Common.Events;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event);
}