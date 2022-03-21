namespace Domain.Entities.ProductAggregate;

public class Part {

    public int Id { get; set; }

    public CatalogProduct Product { get; set; }

    public string Name { get; set; }

    private readonly Dictionary<string, PartAttribute> _attributes = new();

    public IReadOnlyCollection<PartAttribute> Attributes => _attributes.Values.ToList().AsReadOnly();

    public Part(string name, CatalogProduct product) {
        Name = name;
        Product = product;
    }
    
    public void AddAttribute(string attributeName) {
        if (attributeName is null)
            throw new ArgumentNullException(nameof(attributeName));
        if (_attributes.ContainsKey(attributeName)) return;
        _attributes.Add(attributeName, new(-1, Id, attributeName));
    }

}
