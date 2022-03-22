namespace Catalog.Contracts;

public class ProductSummary {

    public Guid Id { get; init; }

    public string Name { get; init; }

    public ProductSummary(Guid id, string name) {
        Id = id;
        Name = name;
    }

}