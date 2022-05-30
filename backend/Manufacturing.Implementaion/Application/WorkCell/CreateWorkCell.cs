using Manufacturing.Implementation.Infrastructure;
using MediatR;

namespace Manufacturing.Implementation.Application.WorkCell;

public class CreateWorkCell {

    public record Command(string Alias, int ProductClass) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly WorkCellRepository _repo;

        public Handler(WorkCellRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            _ = await _repo.Create(request.Alias, request.ProductClass);

        }

    }

}