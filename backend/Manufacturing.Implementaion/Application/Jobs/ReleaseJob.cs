using FluentValidation;
using Manufacturing.Implementation.Infrastructure;
using MediatR;

namespace Manufacturing.Implementation.Application;

public class ReleaseJob {

    public record Command(int Id) : IRequest;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.Id)
                .NotEqual(0)
                .WithMessage("Invalid job id");

        }

    }

    public class Handler : AsyncRequestHandler<Command> {
        private readonly JobRepository _repo;

        public Handler(JobRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            var job = await _repo.GetJobById(request.Id);
            job.ReleaseToProduction();
            await _repo.Save(job);
        }
    }

}