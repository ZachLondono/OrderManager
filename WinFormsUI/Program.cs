using OrderManagment;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WinFormsUI;

internal static class Program {
    
    public static IServiceProvider? ServiceProvider { get; private set; }

    [STAThread]
    static void Main() {
        ConfigureServices();
        ApplicationConfiguration.Initialize();
        Application.Run(new Products());
    }

    static void ConfigureServices() {
        var services = new ServiceCollection();
        services.AddLogging(c => c.AddConsole());
        services.AddOrderManagment();
        ServiceProvider =  services.BuildServiceProvider();
    }

}