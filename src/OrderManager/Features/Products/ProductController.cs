using MediatR;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Products;

public class ProductController {
    
    private readonly ISender _sender;

    public ProductController(ISender sender) {
        _sender = sender;
    }

    public Task<Product> CreateProduct(string name, string description, IEnumerable<string> attributes, CancellationToken cancellationToken = default) {
        return _sender.Send(new CreateProduct.Command(name, description, attributes), cancellationToken);
    }

}
