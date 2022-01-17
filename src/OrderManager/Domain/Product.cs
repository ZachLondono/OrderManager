namespace OrderManager.ApplicationCore.Domain;

public class Product {

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IEnumerable<string> Attributes { get; set; } = Enumerable.Empty<string>();

    public string PricingScript { get; set; } = string.Empty;

    public override string ToString() {
        return $"{ProductId} - {ProductName}";
    }

}
