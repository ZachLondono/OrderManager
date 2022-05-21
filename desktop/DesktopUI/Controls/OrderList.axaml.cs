using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DesktopUI.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace DesktopUI.Controls;

public partial class OrderList : UserControl {

    public static readonly StyledProperty<ObservableCollection<OrderListItem>?> OrdersProperty =
                    AvaloniaProperty.Register<OrderList, ObservableCollection<OrderListItem>?>(nameof(Orders));

    public static readonly StyledProperty<ICommand?> OnSelectedOrderProperty =
                    AvaloniaProperty.Register<OrderList, ICommand?>(nameof(OnSelectedOrder));


    private ObservableCollection<OrderListItem>? _orders;
    public ObservableCollection<OrderListItem>? Orders {
        get => _orders;
        set => SetAndRaise(OrdersProperty, ref _orders, value);
    }

    private ICommand? _onSelectedOrder;
    public ICommand? OnSelectedOrder {
        get => _onSelectedOrder;
        set => SetAndRaise(OnSelectedOrderProperty, ref _onSelectedOrder, value);
    }

    private ItemsControl? _orderList;

    public OrderList() {
        InitializeComponent();

        _orderList = this.FindControl<ItemsControl>("OrderItems");
        

    }

    private void Orders_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
        if (_orderList is null || Orders is null) return;
        _orderList.Items = Orders;
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }

    protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change) {
        base.OnPropertyChanged(change);

        switch (change.Property.Name) {

            case nameof(Orders):
                Orders = change.NewValue.Value as ObservableCollection<OrderListItem>;
                
                if (Orders is null || _orderList is null) break;
                _orderList.Items = Orders;
                Orders.CollectionChanged += Orders_CollectionChanged;
                break;

            case nameof(OnSelectedOrder):
                OnSelectedOrder = change.NewValue.Value as ICommand;
                break;

        }

    }

    public void OrderSelected(object? sender, RoutedEventArgs args) {
        var button = (sender as RadioButton)!;
        if (OnSelectedOrder is null) return;
        OnSelectedOrder.Execute(button.DataContext as OrderListItem);
    }

}
