using Manufacturing.Contracts;
using MediatR;

namespace Manufacturing.Implementaion.Application;

internal class GetJobById {
    
    public record Query(Guid Id) : IRequest<JobDetails>;

    public class Handler : IRequestHandler<Query, JobDetails> {
        public Task<JobDetails> Handle(Query request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}