using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.ApplicationCore.Features.Companies;
using OrderManager.ApplicationCore.Features.Orders;
using OrderManager.ApplicationCore.Features.Products;
using OrderManager.ApplicationCore.Features.Reports;
using OrderManager.ApplicationCore.Features.Scripts;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore;

public static class DependencyInjection {

    public static IServiceCollection AddAppplicationCore(this IServiceCollection services, IConfiguration config) {
        return services
            .AddValidatorsFromAssemblyContaining(typeof(DependencyInjection))
            .AddMediatR(typeof(DependencyInjection).Assembly)
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>))
            .AddControllers()
            .AddTransient(c => {
                return new AppConfiguration() {
                    ConnectionString = config.GetConnectionString("AccessDB") ?? "",
                    ScriptDirectory = config.GetConnectionString("ScriptDir") ?? ""
                };
            });
            
    }

    private static IServiceCollection AddControllers(this IServiceCollection services) {
        // Find all types in this assembly which inherit from BaseController and are not abstract
        IEnumerable<Type> controllers = typeof(DependencyInjection)
                                                    .Assembly
                                                    .GetTypes()
                                                    .Where(t => t.IsSubclassOf(typeof(BaseController)) && !t.IsAbstract);

        // Add all those types to the service collection
        foreach (Type controllerType in controllers) {
            services.AddTransient(controllerType);
        }

        return services;
    }

}
