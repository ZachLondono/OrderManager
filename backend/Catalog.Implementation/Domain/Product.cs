namespace Catalog.Implementation.Domain;

public class Product {

    public Guid Id { get; init; }

    public string Name { get; set; }

    public IReadOnlyCollection<string> Attributes => _attributes.ToList().AsReadOnly();
    private HashSet<string> _attributes { get; set; } = new HashSet<string>();

    public Product(string name) {
        Name = name;
    }

    public void AddAttribute(string name) {
        if (name is null)
            throw new ArgumentNullException(nameof(name));
        if (_attributes.Contains(name))
            throw new ArgumentException($"Attribute already contains value '{name}'", nameof(name));
        _attributes.Add(name);
    }

    public void RemoveAttribute(string name) {
        _attributes.Remove(name);
    }

}
