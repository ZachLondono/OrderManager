using Sales.Implementation.Domain;

namespace Sales.Implementation.Infrastructure;

public record ItemAddedEvent(OrderedItem Item);
public record ItemRemovedEvent(Guid Id);
public record OrderPlacedEvent();
public record OrderConfirmedEvent();
public record OrderCompletedEvent();
public record OrderCanceledEvent();

public class OrderContext {

    private readonly Order _order;
    private readonly List<object> _events;

    public OrderContext(Order order) {
        _order = order;
        _events = new();
    }

    public void AddItem(OrderedItem item) {
        _order.AddItem(item);
        _events.Add(new ItemAddedEvent(item));
    }

    public void RemoveItem(Guid id) {
        _order.RemoveItem(id);
        _events.Add(new ItemRemovedEvent(id));
    }

    public void PlaceOrder() {
        throw new NotImplementedException();
    }

    public void ConfirmOrder() {
        _order.ConfirmOrder();
        _events.Add(new OrderConfirmedEvent());
    }

    public void CompleteOrder() {
        _order.CompleteOrder();
        _events.Add(new OrderCompletedEvent());
    }

    public void VoidOrder() {
        _order.VoidOrder();
        _events.Add(new OrderCanceledEvent());
    }

}
