using Dapper;
using Manufacturing.Contracts;
using MediatR;
using System.Data;

namespace Manufacturing.Implementation.Application;

public class GetJobs {

    public record Query() : IRequest<IEnumerable<JobSummary>>;

    public class Handler : IRequestHandler<Query, IEnumerable<JobSummary>> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<IEnumerable<JobSummary>> Handle(Query request, CancellationToken cancellationToken) {
            
            const string query = @"SELECT [Id], [Name], [Number], [CustomerId], [VendorId], [ItemCount]
                                    FROM [Manufacturing].[Jobs];";

            var jobs = await _connection.QueryAsync<JobSummary>(query);

            return jobs;

        }
    }

}