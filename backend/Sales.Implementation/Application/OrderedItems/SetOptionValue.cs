using MediatR;

namespace Sales.Implementation.Application.OrderedItems;

internal class SetOptionValue {

    public record Command(Guid ItemId, string Option, string Value) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}