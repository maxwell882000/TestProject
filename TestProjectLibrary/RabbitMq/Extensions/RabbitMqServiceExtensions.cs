using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using TestProjectLibrary.RabbitMq.Interfaces;
using IEventBus = TestProjectLibrary.RabbitMq.Interfaces.IEventBus;

namespace TestProjectLibrary.RabbitMq.Extensions
{
    public static class RabbitMqServiceExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IBus>(sp =>
                RabbitHutch.CreateBus(
                    "host=rabbitmqcustom;port=5672;virtualHost=/;username=guest;password=guest;requestedHeartbeat=10"));
            services.AddTransient<IEventBus, EventBus>();
            return services;
        }

        public static IEventBus useRabbitMqSubscription(this IApplicationBuilder app)
        {
            return app.ApplicationServices.GetRequiredService<IEventBus>();
        }
    }
}