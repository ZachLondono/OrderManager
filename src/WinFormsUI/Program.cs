using OrderManager.ApplicationCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.WinFormsUI;

internal static class Program {
    
    public static IServiceProvider? ServiceProvider { get; private set; }

    [STAThread]
    static void Main() {
        ServiceProvider = new ServiceCollection()
            .AddLogging(c => c.AddConsole())
            .AddAppplicationCore()
            .BuildServiceProvider();

        ApplicationConfiguration.Initialize();
        Application.Run(new Products());
    }

    static void SetProfile(ConfigurationProfile profile) {



    }

}