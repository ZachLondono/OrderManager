namespace Domain.Entities.ProductAggregate;

public class Part {

    public int Id { get; set; }

    public CatalogProduct Product { get; set; }

    public string Name { get; set; }

    public Part(string name, CatalogProduct product) {
        Name = name;
        Product = product;
    }

}
