using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DesktopUI.Views;
public partial class CompanyEditorView : UserControl {
    public CompanyEditorView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}
