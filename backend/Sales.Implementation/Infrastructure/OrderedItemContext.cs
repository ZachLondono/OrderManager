using Sales.Implementation.Domain;

namespace Sales.Implementation.Infrastructure;

internal record ItemOptionSet(string Option, string Value);
internal record ItemQtySet(int Qty);

internal class OrderedItemContext {

    private readonly OrderedItem _item;
    private readonly List<object> _events;

    public OrderedItemContext(OrderedItem item) {
        _item = item;
        _events = new();
    }

    public void SetItemOption(string option, string value) => throw new NotImplementedException();

    public void SetQty(int qty) => throw new NotImplementedException();

}