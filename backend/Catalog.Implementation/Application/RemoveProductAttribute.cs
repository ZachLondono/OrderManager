using MediatR;

namespace Catalog.Implementation.Application;

public class RemoveProductAttribute {

    public record Command(Guid ProductId, string Attribute) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
