using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.MainWindow;

namespace OrderManager;
public class App : Application {
    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {

            desktop.MainWindow = new MainWindow.MainWindow {
                DataContext = Program.CreateInstance<MainWindowViewModel>()
            };

        }

        base.OnFrameworkInitializationCompleted();
    }
}
