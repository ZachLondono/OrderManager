using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DesktopUI.ViewModels;
using DesktopUI.Views;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DesktopUI;
public partial class App : Application {

    private static IServiceProvider? _services;

    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {

        ConfigureServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow {
                DataContext = _services!.GetRequiredService<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public static object GetRequiredService(Type type) {
        if (_services is null) throw new InvalidOperationException("Service provider has not been initilized");
        return _services.GetRequiredService(type);
    }

    public static T GetRequiredService<T>() where T : notnull => (T) GetRequiredService(typeof(T));

    private static void ConfigureServiceProvider() {
        var services = ConfigureServices();
        _services = services.BuildServiceProvider();
        if (_services is null) throw new InvalidOperationException("Service provider could not be built");
    }

    private static IServiceCollection ConfigureServices() {
        var services = new ServiceCollection();

        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<LabelFieldEditorViewModel>();
        services.AddTransient<LabelListViewModel>();
        services.AddTransient<EmailTemplateEditorViewModel>();
        services.AddTransient<ReleaseProfileEditorViewModel>();
        services.AddTransient<EmailListViewModel>();
        services.AddInfrastructure();

        return services;
    }

}
