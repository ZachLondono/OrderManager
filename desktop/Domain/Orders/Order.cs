namespace OrderManager.Domain.Orders;

public class Order {

    public int Id { get; init; }

    public string Name { get; init; }

    public string Number { get; init; }

    private readonly List<LineItem> _items;
    public IReadOnlyCollection<LineItem> Items => _items;

    public Order(int id, string name, string number, IEnumerable<LineItem> items) {
        Id = id;
        Name = name;
        Number = number;
        _items = new(items);
    }

}