namespace Catalog.Implementation.Infrastructure.Persistance;

internal class ProductAttribute {

    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Default { get; set; } = string.Empty;

}
