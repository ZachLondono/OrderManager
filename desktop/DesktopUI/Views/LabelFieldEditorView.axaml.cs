using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using DesktopUI.ViewModels;

namespace DesktopUI.Views;
public partial class LabelFieldEditorView : UserControl {

    public LabelFieldEditorView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

    private void Formula_KeyUp(object sender, KeyEventArgs e) {
        if (DataContext is not LabelFieldEditorViewModel vm) return;
        vm.CanSave = true;
    }

}
