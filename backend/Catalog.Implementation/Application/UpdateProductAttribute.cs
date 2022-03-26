using MediatR;

namespace Catalog.Implementation.Application;

public class UpdateProductAttribute {

    public record Command(Guid ProductId, string OldAttribute, string NewAttribute) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
