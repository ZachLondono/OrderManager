using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.ApplicationCore.Features.Orders;
using OrderManager.ApplicationCore.Features.Products;
using OrderManager.ApplicationCore.Infrastructure;
using OrderManager.ApplicationCore.Infrastructure.Implementations;

namespace OrderManager.ApplicationCore;

public static class DependencyInjection {

    public static IServiceCollection AddAppplicationCore(this IServiceCollection services) {

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

        services.AddTransient<ProductController>();
        services.AddTransient<OrderController>();

        services.AddSingleton<IConfigurationProfile, DBConfigurationProfile>();

        services.AddTransient<DataBaseConfiguration>();

        return services;
    }

}
