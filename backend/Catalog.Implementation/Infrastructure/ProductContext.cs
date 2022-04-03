using Catalog.Implementation.Domain;

namespace Catalog.Implementation.Infrastructure;

internal record NameChangeEvent(string Name);
internal record AttributeAddedEvent(string Name, string Default);
internal record AttributeRemovedEvent(string Name);

/// <summary>
/// Keeps track of events that are applied to a product
/// </summary>
public class ProductContext {
    
    private readonly Product _product;
    private readonly List<object> _events = new();

    public int Id => _product.Id;
    public IEnumerable<ProductAttribute> Attributes => _product.Attributes;

    public IReadOnlyCollection<object> Events => _events.AsReadOnly();

    public ProductContext(Product product) {
        _product = product;
    }

    public void SetName(string name) {
        _product.Name = name;
        _events.Add(new NameChangeEvent(name));
    }

    public void AddAttribute(ProductAttribute attribute) {
        _product.AddAttribute(attribute);
        _events.Add(new AttributeAddedEvent(attribute.Name, attribute.Default));
    }

    public void RemoveAttribute(string name) {
        _product.RemoveAttribute(name);
        _events.Add(new AttributeRemovedEvent(name));
    }

}