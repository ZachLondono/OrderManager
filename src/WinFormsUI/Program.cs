using OrderManager.ApplicationCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace OrderManager.WinFormsUI;

internal static class Program {

    public static IServiceProvider? ServiceProvider { get; private set; }

    [STAThread]
    static void Main() {

        IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

        ServiceProvider = new ServiceCollection()
            .AddLogging(c => c.AddConsole())
            .AddAppplicationCore(config)
            .BuildServiceProvider();

        ApplicationConfiguration.Initialize();
        Application.Run(new Testing());
    }

}