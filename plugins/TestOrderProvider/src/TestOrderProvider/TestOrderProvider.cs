using PluginContracts.Interfaces;
using PluginContracts.Models;

namespace TestOrderProvider;

public class TestOrderProvider : INewOrderProvider {
    
    public string PluginName => "Test Order Provider6";

    public int Version => 10;

    public OrderDto GetNewOrder() {

        Console.WriteLine($"Loading order from plugin '{PluginName}', v{Version}");

        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        string number =  $"OT{_rdm.Next(_min, _max)}";

        Console.WriteLine($"Random new order Number {number}");

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
            Number = number,
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
