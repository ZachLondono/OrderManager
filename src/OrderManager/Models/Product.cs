namespace OrderManagment.Models;

public class Product {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IEnumerable<string> Attributes { get; set; } = Enumerable.Empty<string>();

}
