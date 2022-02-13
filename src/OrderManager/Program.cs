using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using MediatR;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            .BuildServiceProvider();

        if (ServiceProvider is null) throw new InvalidProgramException(nameof(ServiceProvider));

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
