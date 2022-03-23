using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Sales.Implementation.Infrastructure;

public static class ConfigureService {

    public static IServiceCollection AddSales(IServiceCollection services) => 
                services.AddMediatR(typeof(ConfigureService).Assembly);

}
