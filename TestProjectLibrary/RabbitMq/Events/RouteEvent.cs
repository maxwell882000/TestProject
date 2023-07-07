using TestProjectLibrary.Db.Entities;
using TestProjectLibrary.RabbitMq.Events.Interfaces;

namespace TestProjectLibrary.RabbitMq.Events;

public class RouteEvent : IEvent
{
    public Guid Id { get; set; }

    public IList<Route> Routes { get; set; }
}