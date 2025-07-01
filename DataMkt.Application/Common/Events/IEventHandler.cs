namespace DataMkt.Application.Common.Events;
public interface IEventHandler<TEvent>
{
    Task HandleAsync(TEvent @event);
}