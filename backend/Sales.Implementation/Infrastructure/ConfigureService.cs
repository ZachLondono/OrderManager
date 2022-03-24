using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Sales.Implementation.Infrastructure;

public static class ConfigureService {

    public static IServiceCollection AddSales(this IServiceCollection services) => 
                services.AddMediatR(typeof(ConfigureService).Assembly)
                        .AddTransient<OrderRepository>()
                        .AddTransient<CompanyRepository>()
                        .AddTransient<OrderedItemRepository>();

}
