using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore.Features.Products;

public class ProductController : BaseController {

    public ProductController(ISender sender) : base(sender) { }

    public Task<Product?> CreateProduct(string name, string description, IEnumerable<string> attributes, CancellationToken cancellationToken = default) {
        return Sender.Send(new CreateProduct.Command(name, description, attributes), cancellationToken);
    }

    public Task<IEnumerable<Product>> GetAllProducts() {
        return Sender.Send(new GetAllProducts.Query());
    }

    public Task<IEnumerable<string>> GetProductAttributes(int productId) {
        return Sender.Send(new GetProductAttributesById.Query(productId));
    }

}
