using Microsoft.Extensions.DependencyInjection;

namespace DataMkt.Application.Common.Events;

public class InMemoryEventPublisher : IEventPublisher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryEventPublisher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<TEvent>(TEvent @event)
    {
        var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
        var handlers = _serviceProvider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            var handleMethod = handlerType.GetMethod("HandleAsync");
            if (handleMethod != null)
            {
                await (Task)handleMethod.Invoke(handler, new object[] { @event })!;
            }
        }
    }
}