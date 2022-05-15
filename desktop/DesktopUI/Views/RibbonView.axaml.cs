using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using DesktopUI.ViewModels;

namespace DesktopUI.Views;
public partial class RibbonView : UserControl {
    public RibbonView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}
