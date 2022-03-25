using Catalog.Implementation.Domain;

namespace Catalog.Implementation.Infrastructure;

internal record NameChangeEvent(string Name);
internal record AttributeAddedEvent(string Name);
internal record AttributeUpdateEvent(string Name);

/// <summary>
/// Keeps track of events that are applied to a product
/// </summary>
public class ProductContext {
    
    private readonly Product _product;
    private readonly List<object> _events = new();

    public IReadOnlyCollection<object> Events => _events.AsReadOnly();

    public ProductContext(Product product) {
        _product = product;
    }

    public void SetName(string name) => throw new NotImplementedException();

    public void AddAttribute(string name) => throw new NotImplementedException();

    public void RemoveAttribute(string name) => throw new NotImplementedException();

}