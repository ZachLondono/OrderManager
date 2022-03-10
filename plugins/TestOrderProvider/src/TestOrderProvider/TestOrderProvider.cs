using PluginContracts.Interfaces;
using PluginContracts.Models;

namespace TestOrderProvider;

public class TestOrderProvider : INewOrderProvider {
    
    public string PluginName => "Test Order Provider";

    public int Version => 1;

    public OrderDto GetNewOrder() {

        return new OrderDto {
            Customer = new() {
                Name = "Customer ABC",
                Address1 = "Addy 1",
                Address2 = "Addy 2",
                Address3 = "Addy 3",
                City = "LA",
                State = "California",
                Zip = "90210",
                Contact = "Bob Saget"
            },
            Name = "Test Order",
            Number = "OT111",
            SupplierId = 1,
            VendorId = 1,
            Products = new List<ProductDto> {
                new() {
                    ProductId = 1,
                    LineNumber = 1,
                    Qty = 2,
                    Options = new() {
                        { "Height", "4.125"},
                        { "Width", "21"},
                        { "Depth", "21"},
                    }
                }
            }
        };

    }
}
