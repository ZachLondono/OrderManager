using Catalog.Implementation.Infrastructure;
using MediatR;

namespace Catalog.Implementation.Application;

public class UpdateProductAttribute {

    public record Command(int ProductId, string OldAttribute, string NewAttribute) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly ProductRepository _repository;

        public Handler(ProductRepository repository) {
            _repository = repository;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            var product = await _repository.GetProductById(request.ProductId);

            var attribute = product.Attributes.Where(a => a.Name.Equals(request.OldAttribute)).FirstOrDefault();

            if (attribute is not null) {
                product.RemoveAttribute(request.OldAttribute);

                attribute.Name = request.NewAttribute;
                product.AddAttribute(attribute);
            }

            await _repository.Save(product);

        }
    }

}
