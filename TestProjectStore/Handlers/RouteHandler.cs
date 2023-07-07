using TestProjectLibrary.RabbitMq.Events;
using TestProjectLibrary.RabbitMq.Handlers.Interfaces;
using TestProjectStore.Repository.Interfaces;
using TestProjectStore.Service.Interfaces;

namespace TestProjectStore.Handlers;

public class RouteHandler : IEventHandler<RouteEvent>
{
    private readonly IRouteService _service;
    private readonly ILogger<RouteHandler> _logger;

    public RouteHandler(ILogger<RouteHandler> logger, IRouteService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task Handle(RouteEvent @event)
    {
        // validate so there is no other destination origin with the same price and timelimit
        _logger.LogInformation($"Consumed Route Event with id: {@event.Id}");
        await _service.Add(@event.Routes);
    }
}