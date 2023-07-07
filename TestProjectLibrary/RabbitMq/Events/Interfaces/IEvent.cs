namespace TestProjectLibrary.RabbitMq.Events.Interfaces;

public interface IEvent
{
    public Guid Id { get; set; }
}