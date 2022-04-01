using Sales.Implementation.Domain;
using System.Collections.ObjectModel;

namespace Sales.Implementation.Infrastructure;

public record ItemAddedEvent(OrderedItem Item);
public record ItemRemovedEvent(int ItemId);
public record OrderPlacedEvent(DateTime TimeStamp);
public record OrderConfirmedEvent(DateTime TimeStamp);
public record OrderCompletedEvent(DateTime TimeStamp);
public record OrderCanceledEvent(DateTime TimeStamp);

public class OrderContext {

    private readonly Order _order;
    private readonly List<object> _events;

    public ReadOnlyCollection<object> Events => _events.AsReadOnly();

    public OrderContext(Order order) {
        _order = order;
        _events = new();
    }

    public void AddItem(OrderedItem item) {
        _order.AddItem(item);
        _events.Add(new ItemAddedEvent(item));
    }

    public void RemoveItem(int id) {
        _order.RemoveItem(id);
        _events.Add(new ItemRemovedEvent(id));
    }

    public void PlaceOrder() {
        throw new NotImplementedException();
    }

    public void ConfirmOrder() {
        _order.ConfirmOrder();
        _events.Add(new OrderConfirmedEvent(DateTime.Now));
    }

    public void CompleteOrder() {
        _order.CompleteOrder();
        _events.Add(new OrderCompletedEvent(DateTime.Now));
    }

    public void VoidOrder() {
        _order.VoidOrder();
        _events.Add(new OrderCanceledEvent(DateTime.Now));
    }

}
