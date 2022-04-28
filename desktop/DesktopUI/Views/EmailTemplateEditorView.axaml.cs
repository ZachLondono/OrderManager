using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using DesktopUI.ViewModels;

namespace DesktopUI.Views;
public partial class EmailTemplateEditorView : UserControl {

    public EmailTemplateEditorView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

    private void Formula_KeyUp(object sender, KeyEventArgs e) {
        if (DataContext is not EmailTemplateEditorViewModel vm) return;
        vm.CanSave = true;
    }

}
