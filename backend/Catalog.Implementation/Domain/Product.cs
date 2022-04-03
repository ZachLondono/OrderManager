using System;

namespace Catalog.Implementation.Domain;

public class Product {

    public int Id { get; init; }

    public string Name { get; set; }

    public IReadOnlyCollection<ProductAttribute> Attributes => _attributes.ToList().AsReadOnly();
    private HashSet<ProductAttribute> _attributes { get; set; } = new();

    public Product(int id, string name) {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be null or empty", nameof(name));
        Name = name;
        Id = id;
    }

    public void AddAttribute(ProductAttribute attribute) {
        if (attribute is null)
            throw new ArgumentNullException(nameof(attribute));
        if (string.IsNullOrWhiteSpace(attribute.Name))
            throw new ArgumentException("Attribute name cannot be empty", nameof(attribute));
        if (_attributes.Any(a => a.Name.Equals(attribute.Name)))
            throw new ArgumentException($"Product already contains attribute '{attribute.Name}'", nameof(attribute));
        _attributes.Add(attribute);
    }

    public void RemoveAttribute(string name) {
        if (name is null)
            throw new ArgumentNullException(nameof(name));
        var attribute = _attributes.Where(a => a.Name.Equals(name)).FirstOrDefault();
        if (attribute is not null) _attributes.Remove(attribute);
    }

}
