using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Manufacturing.Implementation.Infrastructure;

public static class ConfigureServices {

    public static IServiceCollection AddManufacturing(this IServiceCollection services) =>
            services.AddMediatR(typeof(ConfigureServices).Assembly)
                    .AddTransient<JobRepository>();

}
