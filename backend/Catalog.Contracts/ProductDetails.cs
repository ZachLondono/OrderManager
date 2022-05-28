namespace Catalog.Contracts;

public class ProductDetails {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Class { get; set; }

    public Dictionary<string, string> Attributes { get; set; } = new();

}
