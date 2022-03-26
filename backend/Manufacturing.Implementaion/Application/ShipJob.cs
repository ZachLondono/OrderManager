using Manufacturing.Implementation.Infrastructure;
using MediatR;

namespace Manufacturing.Implementation.Application;

internal class ShipJob {

    public record Command(Guid Id) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        private readonly JobRepository _repo;

        public Handler(JobRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            var job = await _repo.GetJobById(request.Id);
            job.Ship();
            await _repo.Save(job);
        }
    }

}
