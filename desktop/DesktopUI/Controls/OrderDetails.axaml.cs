using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Diagnostics;

namespace DesktopUI.Controls;
public partial class OrderDetails : UserControl {

    public static readonly StyledProperty<int> SelectedOrderIdProperty =
                AvaloniaProperty.Register<OrderDetails, int>(nameof(SelectedOrderId));

    private int _selectedOrderId;
    public int SelectedOrderId {
        get => _selectedOrderId;
        set => SetAndRaise(SelectedOrderIdProperty, ref _selectedOrderId, value);
    }

    private TextBlock _selectedOrderText;

    public OrderDetails() {
        InitializeComponent();
        _selectedOrderText = this.FindControl<TextBlock>("SelectedOrderText");
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

    protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change) {
        base.OnPropertyChanged(change);

        switch (change.Property.Name) {

            case nameof(SelectedOrderId):
                SelectedOrderId = (int)(object)change.NewValue.Value;
                _selectedOrderText.Text = SelectedOrderId.ToString();
                break;

        }

    }

}
