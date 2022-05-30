using Manufacturing.Contracts;
using MediatR;

namespace Manufacturing.Implementation.Application.BackLogs;

public class GetBackLog {

    public record Query() : IRequest<BackLog>;

    public class Handler : IRequestHandler<Query, BackLog> {

        private readonly WorkShop.GetBackLog _getBackLog;

        public Handler(WorkShop.GetBackLog getBackLog) {
            _getBackLog = getBackLog;
        }

        public Task<BackLog> Handle(Query request, CancellationToken cancellationToken) {
            return _getBackLog();
        }
    }

}
