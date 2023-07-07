using TestProjectLibrary.RabbitMq.Events.Interfaces;

namespace TestProjectLibrary.RabbitMq.Handlers.Interfaces;

public interface IEventHandler<in TIEvent> where TIEvent : IEvent
{
    Task Handle(TIEvent @event);
}