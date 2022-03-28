using Catalog.Contracts;
using Catalog.Implementation.Infrastructure;
using MediatR;

namespace Catalog.Implementation.Application;

public class GetProducts {

    public record Query() : IRequest<ProductSummary[]>;

    public class Handler : IRequestHandler<Query, ProductSummary[]> {

        private readonly ProductRepository _repository;

        public Handler(ProductRepository repository) {
            _repository = repository;
        }

        public async Task<ProductSummary[]> Handle(Query request, CancellationToken cancellationToken) {
            return await _repository.GetProducts();
        }
    }

}