using Manufacturing.Contracts;
using Manufacturing.Implementation.Infrastructure;
using MediatR;

namespace Manufacturing.Implementation.Application.WorkCell;

public class ScheduleJob {

    public record Command(int WorkCellId, int JobId, DateTime ScheduledDate) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly WorkShop.GetBackLog getBackLog;
        private readonly WorkShop.GetJobWorkCellId getJobWorkCellId;
        private readonly WorkCellRepository _repo;

        public Handler(WorkShop.GetBackLog backLogQuery, WorkShop.GetJobWorkCellId workCellQuery, WorkCellRepository repo) {
            getBackLog = backLogQuery;
            getJobWorkCellId = workCellQuery;
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            // Check if the job is in the backlog, if it is not, it must be assigned to this workcell in order to reschedule it.
            var backlog = await getBackLog();
            if (!backlog.Jobs.Any(item => item.JobId == request.JobId)) {
                
                // workCellId should not be null here since that would mean that job would be in the backlog

                int? workCellId = await getJobWorkCellId(request.JobId);

                if (workCellId is not null && workCellId != request.WorkCellId)
                    throw new InvalidOperationException("The Job is already scheduled in another Work Cell");

            }

            var workCell = await _repo.GetById(request.WorkCellId);

            workCell.ScheduleJob(request.JobId, request.ScheduledDate);

            await _repo.Save(workCell);

        }
    }

}
