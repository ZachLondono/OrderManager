using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using MediatR;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using System;
using Domain;
using Domain.OrderProvider;

namespace OrderManager;

internal class Program {

    private static Program? _instance = null; 

    internal IServiceProvider ServiceProvider { get; private set; }

    private Program() {
        ServiceProvider = new ServiceCollection()
            .AddMediatR(typeof(Program).Assembly)
            .AddDomain()
            .AddSingleton<NewOrderProviderFactory>()
            .BuildServiceProvider();
    }

    public static T GetInstance<T>() {
        if (_instance is null) _instance = new Program();
        return ActivatorUtilities.CreateInstance<T>(_instance.ServiceProvider);
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
