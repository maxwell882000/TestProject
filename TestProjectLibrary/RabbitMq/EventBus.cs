using System.Net.Sockets;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client.Exceptions;
using TestProjectLibrary.RabbitMq.Events.Interfaces;
using TestProjectLibrary.RabbitMq.Extensions;
using TestProjectLibrary.RabbitMq.Handlers.Interfaces;
using IEventBus = TestProjectLibrary.RabbitMq.Interfaces.IEventBus;

namespace TestProjectLibrary.RabbitMq;

public class EventBus : IEventBus
{
    private readonly ILogger<EventBus> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly int _retryCount;
    private readonly IBus _bus;

    public EventBus(ILogger<EventBus> logger,
        IServiceProvider serviceProvider, IBus bus,
        int retryCount = 5)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _serviceProvider = serviceProvider;
        _bus = bus;
        _retryCount = retryCount;
    }

    public void Publish<T>(T @event) where T : IEvent
    {
        var policy = Policy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (ex, time) =>
                {
                    _logger.LogWarning(ex,
                        "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id,
                        $"{time.TotalSeconds:n1}", ex.Message);
                });

        var eventName = @event.GetType().Name;

        _logger.LogWarning("Publish event: {EventId} ({EventName})", @event.Id,
            eventName);
        policy.Execute(() => { _bus.PubSub.Publish<T>(@event); });
    }

    public void Subscribe<T, TH>()
        where T : IEvent
        where TH : IEventHandler<T>
    {
        var eventName = typeof(T).GetGenericTypeName();
        _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName,
            typeof(TH).GetGenericTypeName());

        _bus.PubSub.Subscribe<T>(eventName, msg =>
        {
            _logger.LogWarning($"Event with id {msg.Id} arrived successfully !");
            var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TH>();
            service.Handle(msg);
        });
    }
}