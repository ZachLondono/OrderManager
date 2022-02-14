namespace Domain.Entities.ProductAggregate;

public class CatalogProduct {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    private readonly HashSet<string> _attributes = new();

    public IReadOnlyCollection<string> Attributes => _attributes.ToList().AsReadOnly();

    private readonly Dictionary<string, Part> _parts = new();

    public IReadOnlyCollection<Part> Parts => _parts.Values.ToList().AsReadOnly();

    public void AddAttribute(string attribute) {
        if (attribute is null)
            throw new ArgumentNullException(nameof(attribute));
        if (_attributes.Contains(attribute)) return;
        _attributes.Add(attribute);
    }

    public void AddPart(Part part) {
        if (part is null)
            throw new ArgumentNullException(nameof(part));
        if (_parts.ContainsKey(part.Name)) return;
        _parts.Add(part.Name, part);
    }

}
