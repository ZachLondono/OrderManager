using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.MainWindow;
using System;

namespace OrderManager;
public class App : Application {
    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {

        if (Program.ServiceProvider is null) throw new InvalidProgramException("ServiceProvider is null");

        ISender? sender = Program.ServiceProvider?.GetService<ISender>();

        if (sender is null) throw new InvalidProgramException("Unable to get implementation of ISender");

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow.MainWindow {
                DataContext = new MainWindowViewModel(sender),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
