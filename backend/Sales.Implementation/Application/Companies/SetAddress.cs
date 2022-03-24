using MediatR;

namespace Sales.Implementation.Application.Companies;

internal class SetAddress {

    public record Command(Guid CompanyId, string Line1, string Line2, string Line3, string City, string State, string Zip) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
