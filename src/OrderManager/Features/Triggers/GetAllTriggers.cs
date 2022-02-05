using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore.Features.Triggers;

public class GetAllTriggers {

    public record Query() : IRequest<IEnumerable<Trigger>>;

    internal class Handler : IRequestHandler<Query, IEnumerable<Trigger>> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public Task<IEnumerable<Trigger>> Handle(Query request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

    }

}
