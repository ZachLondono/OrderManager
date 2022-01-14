using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.ApplicationCore.Features.Orders;
using OrderManager.ApplicationCore.Features.Products;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore;

public static class DependencyInjection {

    public static IServiceCollection AddAppplicationCore(this IServiceCollection services) {;
        //services.AddScoped(typeof(IValidator<Features.Products.Create.Command>), typeof(Features.Products.Create.Validator));
        //services.AddScoped(typeof(IValidator<Features.Orders.Create.Command>), typeof(Features.Orders.Create.Validator));
        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
        //services.AddValidatorsFromAssemblyContaining<Features.Products.Create.Validator>();
        //services.AddValidatorsFromAssemblyContaining<Features.Orders.Create.Validator>();

        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

        services.AddTransient<ProductController>();
        services.AddTransient<OrderController>();

        return services;
    }

}
