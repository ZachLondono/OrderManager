using Catalog.Contracts;
using MediatR;

namespace Catalog.Implementation.Application;

internal class Catalog {

    private readonly ISender _sender;

    public Catalog(ISender sender) {
        _sender = sender;
    }

    internal Task<IEnumerable<ProductSummary>> GetProducts() {
        return _sender.Send(new GetProducts.Query());
    }

    internal Task<ProductDetails> GetProductDetails(int id) {
        return _sender.Send(new GetProductDetails.Query(id));
    }

}
