using System.Linq;

namespace Domain.Entities;

public class CatalogProduct {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    private readonly HashSet<string> _attributes = new();

    public IReadOnlyCollection<string> Attributes => _attributes.ToList().AsReadOnly();

    public void AddAttribute(string attribute) {
        if (_attributes.Contains(attribute)) return;
        _attributes.Add(attribute);
    }

}
