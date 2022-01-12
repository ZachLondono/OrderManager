using MediatR;
using OrderManagment.Models;

namespace OrderManagment.Features.Products;

public class ProductController {
    
    private readonly ISender _sender;

    public ProductController(ISender sender) {
        _sender = sender;
    }

    public async Task<Product> CreateProduct(string name, string description, IEnumerable<string> attributes) {
        Product product = await _sender.Send(new Create.Command(name, description, attributes));
        return product;
    }

}
