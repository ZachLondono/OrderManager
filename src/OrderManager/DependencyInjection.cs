using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderManagment.Features.Orders;
using OrderManagment.Features.Products;
using OrderManagment.Infrastructure;

namespace OrderManagment;

public static class DependencyInjection {

    public static IServiceCollection AddOrderManagment(this IServiceCollection services) {;
        services.AddScoped(typeof(IValidator<Features.Products.Create.Command>), typeof(Features.Products.Create.Validator));
        services.AddScoped(typeof(IValidator<Features.Orders.Create.Command>), typeof(Features.Orders.Create.Validator));

        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

        services.AddTransient<ProductController>();
        services.AddTransient<OrderController>();

        return services;
    }

}
