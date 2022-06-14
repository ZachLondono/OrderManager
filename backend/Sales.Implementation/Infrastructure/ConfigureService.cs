using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sales.Contracts;

namespace Sales.Implementation.Infrastructure;

public static class ConfigureService {

    public static IServiceCollection AddSales(this IServiceCollection services, Func<SalesSettings> configureSettings) => 
        services.AddMediatR(typeof(ConfigureService).Assembly)
                .AddTransient<OrderRepository>()
                .AddTransient<CompanyRepository>()
                .AddTransient<OrderedItemRepository>()
                .AddTransient(s => configureSettings());

}
