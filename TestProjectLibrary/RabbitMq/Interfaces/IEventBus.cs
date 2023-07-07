using TestProjectLibrary.RabbitMq.Events;
using TestProjectLibrary.RabbitMq.Events.Interfaces;
using TestProjectLibrary.RabbitMq.Handlers.Interfaces;

namespace TestProjectLibrary.RabbitMq.Interfaces;

public interface IEventBus
{
    void Publish<T>(T @event) where T : IEvent;

    void Subscribe<T, TH>()
        where T : IEvent
        where TH : IEventHandler<T>;
}