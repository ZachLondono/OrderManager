using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;


namespace OrderManager.Features.Ribbon.ReleaseProfiles;
public partial class ProfileEditorDialog : Window {

    public ProfileEditorDialog() {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif

        Button transferRightBtn = this.FindControl<Button>("TransferRightBtn");
        transferRightBtn.Content = "->";

        Button transferLeftBtn = this.FindControl<Button>("TransferLeftBtn");
        transferLeftBtn.Content = "<-";

    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}
