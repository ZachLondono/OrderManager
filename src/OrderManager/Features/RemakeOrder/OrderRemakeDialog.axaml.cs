using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OrderManager.Features.RemakeOrder;
public partial class OrderRemakeDialog : Window {
    public OrderRemakeDialog() {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}
