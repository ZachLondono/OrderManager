using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using MediatR;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using System;
using Domain;

namespace OrderManager;

internal class Program {

    public static IServiceProvider? ServiceProvider { get; private set; }

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) {

        ServiceProvider = new ServiceCollection()
            .AddMediatR(typeof(Program).Assembly)
            .AddDomain()
            .BuildServiceProvider();

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
