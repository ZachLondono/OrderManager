using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OrderManager.Features.OrderDetails.EmptyDetails;
public partial class EmptyOrderDetailsView : UserControl {
    public EmptyOrderDetailsView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}
