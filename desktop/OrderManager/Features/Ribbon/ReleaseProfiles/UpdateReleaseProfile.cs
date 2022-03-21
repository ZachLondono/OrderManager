using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManager.Features.Ribbon.ReleaseProfiles;

public class UpdateReleaseProfile {

    public record Command(ReleaseProfileEventDomain Profile) : IRequest;

    public class Handler : IRequestHandler<Command> {

        private readonly ILogger<Handler> _logger;
        private readonly ReleaseProfileRepository _repository;

        public Handler(ILogger<Handler> logger, ReleaseProfileRepository repository) {
            _logger = logger;
            _repository = repository;
        }

        public Task<Unit> Handle(Command request, CancellationToken cancellationToken) {

            _logger.LogTrace("Handling release profile save request");
            _repository.Save(request.Profile);

            return Unit.Task;

        }

    }

}