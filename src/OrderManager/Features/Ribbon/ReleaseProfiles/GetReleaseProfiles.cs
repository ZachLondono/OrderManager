using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using Dapper;

namespace OrderManager.Features.Ribbon.ReleaseProfiles;

public class GetReleaseProfiles {

    public record Query() : IRequest<IEnumerable<ReleaseProfile>>;

    public class Handler : IRequestHandler<Query, IEnumerable<ReleaseProfile>> {

        private readonly ILogger<Handler> _logger;
        private readonly IDbConnection _connection;

        public Handler(ILogger<Handler> logger, IDbConnection connection) {
            _logger = logger;
            _connection = connection;
        }

        public Task<IEnumerable<ReleaseProfile>> Handle(Query request, CancellationToken cancellationToken) {

            _logger.LogTrace("Handling request for release profiles");

            const string query = "SELECT [Id], [Name] FROM [ReleaseProfiles];";

            var profiles = _connection.Query<ReleaseProfile>(query);

            return Task.FromResult(profiles);

        }
    }

}