using System;

namespace Catalog.Implementation.Domain;

public class Product {

    public Guid Id { get; init; }

    public string Name { get; set; }

    public IReadOnlyCollection<string> Attributes => _attributes.ToList().AsReadOnly();
    private HashSet<string> _attributes { get; set; } = new HashSet<string>();

    public Product(Guid id, string name) {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be null or empty", nameof(name));
        Name = name;
        Id = id;
    }

    public void AddAttribute(string name) {
        if (name is null)
            throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Attribute name cannot be empty", nameof(name));
        if (_attributes.Contains(name))
            throw new ArgumentException($"Attribute already contains value '{name}'", nameof(name));
        _attributes.Add(name);
    }

    public void RemoveAttribute(string name) {
        if (name is null)
            throw new ArgumentNullException(nameof(name));
        _attributes.Remove(name);
    }

}
