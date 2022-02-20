using Avalonia;
using Avalonia.Markup.Xaml;
using AvaloniaUI.Ribbon;

namespace OrderManager.MainWindow;
public partial class MainWindow : RibbonWindow {

    public MainWindow() {
        InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

}
