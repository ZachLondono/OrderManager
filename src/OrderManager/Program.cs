using Avalonia;
using MediatR;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using System;
using Domain;
using OrderManager.Features.LoadOrders;
using System.Diagnostics;
using System.Reflection;
using OrderManager.Shared;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using Microsoft.Data.Sqlite;
using OrderManager.Features.Ribbon.ReleaseProfiles;

namespace OrderManager;

internal class Program {

    private static Program? _instance = null; 

    internal IServiceProvider ServiceProvider { get; private set; }

    private readonly ILogger<Program>? _logger;

    private Program() {

        SqlMapper.AddTypeHandler(new GuidHandler());

        ServiceProvider = new ServiceCollection()
            .AddMediatR(typeof(Program).GetTypeInfo().Assembly)
            .AddDomain()
            .AddSingleton<MainWindow.MainWindow>()
            .AddSingleton<NewOrderProviderFactory>()
            .AddSingleton<ApplicationContext>()
            .AddLogging(loggerBuilder => {
                loggerBuilder.AddConsole();
                loggerBuilder.AddDebug();
                loggerBuilder.SetMinimumLevel(LogLevel.Trace);
            })
            .AddTransient<IDbConnection>(s => new SqliteConnection(s.GetRequiredService<ConnectionStringManager>().GetConnectionString))
            .AddTransient<ReleaseProfileRepository>()
            .BuildServiceProvider();

        _logger = ServiceProvider.GetService<ILogger<Program>>();

    }

    public static T CreateInstance<T>() {
        if (_instance is null) _instance = new Program();
        if (_instance._logger is not null) _instance._logger.LogInformation("Getting instance of type '{0}'", typeof(T));
        return ActivatorUtilities.CreateInstance<T>(_instance.ServiceProvider);
    }

    public static T GetService<T>() {
        if (_instance is null) _instance = new Program();
        if (_instance._logger is not null) _instance._logger.LogInformation("Getting service of type '{0}'", typeof(T));
        var service = _instance.ServiceProvider.GetService<T>();
        if (service is null) throw new InvalidOperationException($"Could not find a sutible service for type '{typeof(T)}'");
        return service;
    }

    public static object GetInstance(Type type) {
        if (_instance is null) _instance = new Program();
        return ActivatorUtilities.CreateInstance(_instance.ServiceProvider, type);
    }

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) {
        BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
}
