using Catalog.Contracts;
using MediatR;

namespace Catalog.Implementation.Application;

public class GetProductDetails {

    public record Query(Guid ProductId) : IRequest<ProductDetails>;

    public class Handler : IRequestHandler<Query, ProductDetails> {
        public Task<ProductDetails> Handle(Query request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
