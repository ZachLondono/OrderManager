namespace Catalog.Contracts;

public class ProductDetails {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Dictionary<string, string> Attributes { get; set; } = new();

}
