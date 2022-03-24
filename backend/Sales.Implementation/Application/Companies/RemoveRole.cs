using MediatR;

namespace Sales.Implementation.Application.Companies;

internal class RemoveRole {

    public record Command(Guid CompanyId, string Role) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}