using Manufacturing.Contracts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Manufacturing.Implementation.Infrastructure;

public static class ConfigureServices {

    public static IServiceCollection AddManufacturing(this IServiceCollection services, Func<ManufacturingSettings> configureSettings) =>
        services.AddMediatR(typeof(ConfigureServices).Assembly)
                .AddTransient<JobRepository>()
                .AddTransient<WorkCellRepository>()
                .AddTransient<WorkShopRepository>()
                .AddTransient(s => configureSettings())
                .AddTransient<WorkShop.GetBackLog>((s) =>
                    s.GetRequiredService<WorkShopRepository>().GetBackLog)
                .AddTransient<WorkShop.GetJobWorkCellId>((s) =>
                    s.GetRequiredService<WorkShopRepository>().GetJobWorkCellById);

}
