using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Splitify.BuildingBlocks.EventBus;

namespace Splitify.EventBus.MassTransit
{
    public static class RabbitMQRegistrator
    {
        public static IServiceCollection AddMassTransitRabbitMQ(
            this IServiceCollection services,
            Action<RabbitMQConfiguration> config)
        {
            var rabbitMQConfiguration = new RabbitMQConfiguration();
            config.Invoke(rabbitMQConfiguration);

            services.AddMassTransit(c =>
            {
                c.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(rabbitMQConfiguration.Host, rabbitMQConfiguration.VirtualHost, h => {
                        h.Username(rabbitMQConfiguration.Username);
                        h.Password(rabbitMQConfiguration.Password);
                    });

                    cfg.ConfigureEndpoints(ctx);
                });
            });

            return services.AddScoped<IEventBus, MassTransitEventBus>();
        }
    }
}