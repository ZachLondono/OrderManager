namespace PluginContracts.Models;

public class OrderDto {

    public string Number { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int SupplierId { get; set; }
    public CompanyDto? Supplier { get; set; }

    public int VendorId { get; set; }
    public CompanyDto? Vendor { get; set; }

    public CompanyDto Customer { get; set; } = default!;

    public List<ProductDto> Products { get; set; } = new();

}