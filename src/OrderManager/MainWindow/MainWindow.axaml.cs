using Avalonia;
using Avalonia.Markup.Xaml;
using AvaloniaUI.Ribbon;
using OrderManager.Features.LoadOrders;
using ReactiveUI;
using System.Diagnostics;

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
