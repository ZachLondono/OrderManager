using Manufacturing.Contracts;
using MediatR;

namespace Manufacturing.Implementation.Application;

internal class GetJobs {

    public record Query() : IRequest<JobSummary[]>;

    public class Handler : IRequestHandler<Query, JobSummary[]> {
        public Task<JobSummary[]> Handle(Query request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}