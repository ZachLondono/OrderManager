using Catalog.Contracts;
using MediatR;

namespace Catalog.Implementation.Application;

internal class GetProducts {

    public record Query() : IRequest<ProductSummary[]>;

    public class Handler : IRequestHandler<Query, ProductSummary[]> {
        public Task<ProductSummary[]> Handle(Query request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}