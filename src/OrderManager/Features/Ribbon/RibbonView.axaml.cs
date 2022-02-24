using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OrderManager.Features.Ribbon;
public partial class RibbonView : UserControl {
    public RibbonView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}
