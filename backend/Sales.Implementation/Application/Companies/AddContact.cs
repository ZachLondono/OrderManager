using MediatR;

namespace Sales.Implementation.Application.Companies;

internal class AddContact {

    public record Command(Guid CompanyId, string Name, string? Email, string? Phone) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
