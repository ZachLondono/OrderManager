using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Diagnostics;

namespace OrderManager.Features.OrderDetails.CompanyDisplay;
public partial class CompanyDisplayView : UserControl {
    public CompanyDisplayView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}
