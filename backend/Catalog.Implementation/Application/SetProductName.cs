using Catalog.Implementation.Infrastructure;
using MediatR;

namespace Catalog.Implementation.Application;

public class SetProductName {

    public record Command(Guid ProductId, string Name) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly ProductRepository _repository;

        public Handler(ProductRepository repository) {
            _repository = repository;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            var product = await _repository.GetProductById(request.ProductId);

            product.SetName(request.Name);

            await _repository.Save(product);

        }
    }

}
