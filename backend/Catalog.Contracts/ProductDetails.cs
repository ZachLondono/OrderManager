namespace Catalog.Contracts;

public class ProductDetails {

    public Guid Id { get; init; }

    public string Name { get; init; }

    public string[] Attributes { get; init; }

    public ProductDetails(Guid id, string name, string[] attributes) {
        Id = id;
        Name = name;
        Attributes = attributes;
    }

}
