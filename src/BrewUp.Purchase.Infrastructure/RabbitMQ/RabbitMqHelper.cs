using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Purchase.Infrastructure.RabbitMQ;

public static class RabbitMqHelper
{
    public static IServiceCollection AddPurchaseRabbitMq(
        this IServiceCollection services,
        RabbitMqSettings rabbitMqSettings)
    {
        var serviceProvider = services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        var configuration = new RabbitMQConfiguration(
            rabbitMqSettings.Host,
            rabbitMqSettings.Username,
            rabbitMqSettings.Password,
            rabbitMqSettings.ExchangeCommandName,
            rabbitMqSettings.ExchangeEventName,
            "brewup-purchase");

        var connectionFactory = new RabbitMQConnectionFactory(configuration, loggerFactory);

        services.TryAddSingleton(connectionFactory);
        services.AddMufloneTransportRabbitMQ(loggerFactory, configuration);

        // Register message handlers
        services.AddPurchaseMessageHandlers();

        return services;
    }
}
