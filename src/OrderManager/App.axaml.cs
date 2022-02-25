using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using OrderManager.MainWindow;

namespace OrderManager;
public class App : Application {
    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted() {

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {

            var window = Program.GetService<MainWindow.MainWindow>();
            window.DataContext = Program.CreateInstance<MainWindowViewModel>();
            desktop.MainWindow = window;

        }

        base.OnFrameworkInitializationCompleted();
    }
}
