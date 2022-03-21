using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OrderManager.Features.OrderDetails.FilledDetails;
public partial class FilledOrderDetailsView : UserControl {
    public FilledOrderDetailsView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}
