using Authentication.Consumers;
using Contract;
using MassTransit;

namespace Authentication.Extensions
{
    public static class MassTransitExtension
    {
        public static IServiceCollection AddMassTransitExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                var rabbitMqSetting = configuration.GetSection(nameof(RabbitMQSetting)).Get<RabbitMQSetting>();

                if (rabbitMqSetting == null ||
                string.IsNullOrWhiteSpace(rabbitMqSetting.Username) ||
                string.IsNullOrWhiteSpace(rabbitMqSetting.Password))
                {
                    throw new ArgumentNullException("RabbitMQ settings are invalid or incomplete.");
                }

                x.AddConsumer<AuthenticationConsume>();

                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"{rabbitMqSetting.Host}/"), h =>
                    {
                        h.Username(rabbitMqSetting.Username);
                        h.Password(rabbitMqSetting.Password);
                    });

                    cfg.ReceiveEndpoint("auth-request", e =>
                    {
                        e.ConfigureConsumer<AuthenticationConsume>(context);
                    });
                });

            });

            return services;
        }
    }
}
