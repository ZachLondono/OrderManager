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

namespace OrderManager;

internal class Program {

    private static Program? _instance = null; 

    internal IServiceProvider ServiceProvider { get; private set; }

    private Program() {
        ServiceProvider = new ServiceCollection()
            .AddMediatR(typeof(Program).GetTypeInfo().Assembly)
            .AddDomain()
            .AddSingleton<MainWindow.MainWindow>()
            .AddSingleton<NewOrderProviderFactory>()
            .AddSingleton<ApplicationContext>()
            .BuildServiceProvider();
    }

    public static T CreateInstance<T>() {
        if (_instance is null) _instance = new Program();
        Debug.WriteLine($"Getting instance of type '{typeof(T)}'");
        return ActivatorUtilities.CreateInstance<T>(_instance.ServiceProvider);
    }

    public static T GetService<T>() {
        if (_instance is null) _instance = new Program();
        Debug.WriteLine($"Getting service of type '{typeof(T)}'");
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
