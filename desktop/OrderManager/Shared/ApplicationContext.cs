using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Shared;

public class ApplicationContext {

    /// <summary>
    /// Event is invoked when an order is selected
    /// </summary>
    public event OrderSelectedHandler? OrderSelectedEvent;
    public record OrderSelectedEventArgs(Guid SelectedId);
    public delegate Task OrderSelectedHandler(object sender, OrderSelectedEventArgs e);

    private Guid? _selectedId = null;
    public Guid? SelectedOrderId {
        get => _selectedId;
        set {
            _selectedId = value;
            if (_selectedId is not null)
                OrderSelectedEvent?.Invoke(this, new((Guid)_selectedId));
        }
    }

    /// <summary>
    /// Event is invoked when an order is added to the application context
    /// </summary>
    public event OrderListUpdateHandler? OrderListUpdateEvent;
    public record OrderAddedEventArgs(Guid newId);
    public delegate Task OrderListUpdateHandler(object sender, OrderAddedEventArgs e);

    private readonly List<Guid> _orderList = new();
    public IReadOnlyCollection<Guid> OrderList {
        get => _orderList.AsReadOnly();
    }

    public void AddOrder(Guid orderId) {
        _orderList.Add(orderId);
        OrderListUpdateEvent?.Invoke(this, new(orderId));
    }

    public void RemoveOrder(Guid orderId) {
        _orderList.Remove(orderId);
        OrderListUpdateEvent?.Invoke(this, new(orderId));
    }

}
