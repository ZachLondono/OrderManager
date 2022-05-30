using Manufacturing.Implementation.Infrastructure;
using MediatR;

namespace Manufacturing.Implementation.Application.WorkCell;

public class RemoveWorkCell {

    public record Command(int WorkCellId) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly WorkCellRepository _repo;

        public Handler(WorkCellRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            await _repo.Remove(request.WorkCellId);

        }

    }

}