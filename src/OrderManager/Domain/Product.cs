namespace OrderManager.ApplicationCore.Domain;

public class Product {

    public int ProductId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IEnumerable<string> Attributes { get; set; } = Enumerable.Empty<string>();

}
