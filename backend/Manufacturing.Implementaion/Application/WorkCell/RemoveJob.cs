using Manufacturing.Implementation.Infrastructure;
using MediatR;

namespace Manufacturing.Implementation.Application.WorkCell;

public class RemoveJob {
    
    public record Command(int WorkCellId, int JobId) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly WorkCellRepository _repo;

        public Handler(WorkCellRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            var workCell = await _repo.GetById(request.WorkCellId);

            workCell.RemoveJob(request.JobId);

            await _repo.Save(workCell);

        }

    }

}
