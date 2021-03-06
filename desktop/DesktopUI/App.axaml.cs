using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DesktopUI.ViewModels;
using DesktopUI.Views;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DesktopUI;
public partial class App : Application {

    private static IServiceProvider? _services;

    public static Window? MainWindow { get; private set; }

    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {

        ConfigureServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            
            MainWindow = new MainWindow {
                DataContext = _services!.GetRequiredService<MainWindowViewModel>()
            };

            desktop.MainWindow = MainWindow;

        }

        base.OnFrameworkInitializationCompleted();
    }

    public static object GetRequiredService(Type type) {
        if (_services is null) throw new InvalidOperationException("Service provider has not been initilized");
        return _services.GetRequiredService(type);
    }

    public static T GetRequiredService<T>() where T : notnull => (T) GetRequiredService(typeof(T));

    private static void ConfigureServiceProvider() {

        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var services = ConfigureServices(config);
        _services = services.BuildServiceProvider();
        if (_services is null) throw new InvalidOperationException("Service provider could not be built");
    }

    private static IServiceCollection ConfigureServices(IConfiguration config) {
        var services = new ServiceCollection();

        services.AddToolWindows();
        services.AddViewModels();
        services.AddInfrastructure(config);

        return services;
    }

}
