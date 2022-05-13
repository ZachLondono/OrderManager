using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using DesktopUI.ViewModels;

namespace DesktopUI.Views;
public partial class ProductDesignerView : UserControl {

    public ProductDesignerView() {
        InitializeComponent();

    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

    private void Attribute_KeyUp(object sender, KeyEventArgs e) {
        if (DataContext is not ProductDesignerViewModel vm) return;
        vm.CanSave = true;
    }

}
