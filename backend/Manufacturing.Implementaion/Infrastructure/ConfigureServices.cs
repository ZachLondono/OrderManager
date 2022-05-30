using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Manufacturing.Implementation.Infrastructure;

public static class ConfigureServices {

    public static IServiceCollection AddManufacturing(this IServiceCollection services) =>
            services.AddMediatR(typeof(ConfigureServices).Assembly)
                    .AddTransient<JobRepository>()
                    .AddTransient<WorkCellRepository>()
                    .AddTransient<WorkShopRepository>()
                    .AddTransient<Contracts.WorkShop.GetBackLog>((s) =>
                        s.GetRequiredService<WorkShopRepository>().GetBackLog)
                    .AddTransient<Contracts.WorkShop.GetJobWorkCellId>((s) =>
                        s.GetRequiredService<WorkShopRepository>().GetJobWorkCellById);

}
