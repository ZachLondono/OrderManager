using Manufacturing.Implementation.Infrastructure;
using MediatR;

namespace Manufacturing.Implementation.Application.WorkCell;

public class UpdateWorkCell {

    public record Command(int WorkCellId, string? Alias, int? ExpectedMaxOutput) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly WorkCellRepository _repo;

        public Handler(WorkCellRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            var workCell = await _repo.GetById(request.WorkCellId);

            if (request.Alias is not null)
                workCell.SetAlias(request.Alias);

            if (request.ExpectedMaxOutput is not null)
                workCell.SetExpectedMaxOutput((int) request.ExpectedMaxOutput);

            await _repo.Save(workCell);

        }

    }

}
