using Sales.Implementation.Domain;

namespace Sales.Implementation.Infrastructure;

internal record ItemOptionSet(string Option, string Value);
internal record ItemQtySet(int Qty);

public class OrderedItemContext {

    private readonly OrderedItem _item;
    private readonly List<object> _events;
    public IReadOnlyCollection<object> Events => _events.AsReadOnly();

    public Guid Id => _item.Id;
    public int Qty => _item.Quantity;

    public OrderedItemContext(OrderedItem item) {
        _item = item;
        _events = new();
    }

    public void SetItemOption(string option, string value) {
        _item.SetOption(option, value);
        _events.Add(new ItemOptionSet(option, value));
    }

    public void SetQty(int qty) {
        _item.SetQuantity(qty);
        _events.Add(new ItemQtySet(qty));
    }

}