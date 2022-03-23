using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Manufacturing.Implementaion.Infrastructure;

public static class ConfigureServices {

    public static IServiceCollection AddManufacturing(IServiceCollection services) =>
            services.AddMediatR(typeof(ConfigureServices).Assembly);

}
