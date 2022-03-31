namespace Catalog.Implementation.Infrastructure.Persistance;

internal class Product {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

}

internal class ProductAttribute {

    public int Id { get; set; }

    public int ProductId { get; set; }

    public string Option { get; set; } = string.Empty;

    public string Default { get; set; } = string.Empty;

}
