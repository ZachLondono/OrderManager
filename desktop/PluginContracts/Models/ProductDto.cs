namespace PluginContracts.Models;

public class ProductDto {

    public int ProductId { get; set; }

    public int Qty { get; set; }

    public int LineNumber { get; set; }

    public Dictionary<string, string> Options { get; set; } = new();

}