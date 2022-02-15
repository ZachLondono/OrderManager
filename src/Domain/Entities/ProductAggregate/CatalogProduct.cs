namespace Domain.Entities.ProductAggregate;

public class CatalogProduct {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    private readonly Dictionary<string, ProductAttribute> _attributes = new();

    public IReadOnlyCollection<ProductAttribute> Attributes => _attributes.Values.ToList().AsReadOnly();

    private readonly Dictionary<string, Part> _parts = new();

    public IReadOnlyCollection<Part> Parts => _parts.Values.ToList().AsReadOnly();

    public void AddAttribute(string attributeName) {
        if (attributeName is null)
            throw new ArgumentNullException(nameof(attributeName));
        if (_attributes.ContainsKey(attributeName)) return;
        _attributes.Add(attributeName, new(-1, Id, attributeName));
    }

    public void AddPart(Part part) {
        if (part is null)
            throw new ArgumentNullException(nameof(part));
        if (_parts.ContainsKey(part.Name)) return;
        _parts.Add(part.Name, part);
    }

}
