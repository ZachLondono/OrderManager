using Sales.Implementation.Domain;

namespace Sales.Implementation.Infrastructure;

public record ItemAddedEvent(OrderedItem Item);
public record ItemRemovedEvent(Guid Id);
public record ItemUpdatedEvent(OrderedItem Item);
public record ItemOptionUpdatedEvent(Guid ItemId, string option, string value);
public record OrderPlacedEvent();
public record OrderCanceledEvent();

internal class OrderContext {

    private readonly Order _order;
    private readonly List<object> _events;

    public OrderContext(Order order) {
        _order = order;
        _events = new();
    }

    public void AddItem(OrderedItem item) => throw new NotImplementedException();

    public void RemoveItem(Guid id) => throw new NotImplementedException();

    public void UpdateItem(OrderedItem item) => throw new NotImplementedException();

    public void SetItemOption(Guid id, string option, string value) => throw new NotImplementedException();

    public void PlaceOrder() => throw new NotImplementedException();

    public void VoidOrder() => throw new NotImplementedException();

}
