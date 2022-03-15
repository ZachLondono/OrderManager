using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManager.Features.Ribbon.ReleaseProfiles;

public class CreateNewReleaseProfile {

    public record Command(string Name) : IRequest<ReleaseProfileEventDomain>;

    public class Handler : IRequestHandler<Command, ReleaseProfileEventDomain> {

        private readonly ILogger<Handler> _logger;
        private readonly ReleaseProfileRepository _repository;

        public Handler(ILogger<Handler> logger, ReleaseProfileRepository repository) {
            _logger = logger;
            _repository = repository;
        }

        public Task<ReleaseProfileEventDomain> Handle(Command request, CancellationToken cancellationToken) {

            _logger.LogTrace("Handling create new release profile request");

            ReleaseProfileEventDomain profile = new(new(Guid.NewGuid(), request.Name));

            _repository.Save(profile);

            return Task.FromResult(profile);

        }
    }

}