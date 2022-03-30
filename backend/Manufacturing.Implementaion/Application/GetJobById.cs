using Manufacturing.Contracts;
using Manufacturing.Implementation.Infrastructure;
using MediatR;

namespace Manufacturing.Implementation.Application;

internal class GetJobById {
    
    public record Query(Guid Id) : IRequest<JobDetails>;

    public class Handler : IRequestHandler<Query, JobDetails> {
        
        private readonly JobRepository _repo;

        public Handler(JobRepository repo) {
            _repo = repo;
        }

        public async Task<JobDetails> Handle(Query request, CancellationToken cancellationToken) {
            var job = await _repo.GetJobById(request.Id);
            return job.Details();
        }
    }

}