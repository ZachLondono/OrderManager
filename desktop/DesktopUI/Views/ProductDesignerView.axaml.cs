using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DesktopUI.Views;
public partial class ProductDesignerView : UserControl {

    public ProductDesignerView() {
        InitializeComponent();

    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

}
