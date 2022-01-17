using OrderManager.ApplicationCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.WinFormsUI;

internal static class Program {

    private static readonly string _scriptDir = @"C:\Users\Zachary Londono\source\repos\OrderManager\src\WinFormsUI\Scripts\";
    private static readonly string _connString = "Provider=Microsoft.ace.OLEDB.12.0; Data Source='C:\\Users\\Zachary Londono\\Desktop\\OrderManager.accdb';";

    public static IServiceProvider? ServiceProvider { get; private set; }

    [STAThread]
    static void Main() {

        AppConfiguration config = new() {
            ConnectionString = _connString,
            ScriptDirectory = _scriptDir
        };

        ServiceProvider = new ServiceCollection()
            .AddLogging(c => c.AddConsole())
            .AddAppplicationCore()
            .AddSingleton(config)
            .BuildServiceProvider();

        ApplicationConfiguration.Initialize();
        Application.Run(new Testing());
    }

}