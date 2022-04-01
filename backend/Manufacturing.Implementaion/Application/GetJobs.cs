using Dapper;
using Manufacturing.Contracts;
using MediatR;
using System.Data;

namespace Manufacturing.Implementation.Application;

internal class GetJobs {

    public record Query() : IRequest<JobSummary[]>;

    public class Handler : IRequestHandler<Query, JobSummary[]> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<JobSummary[]> Handle(Query request, CancellationToken cancellationToken) {
            
            const string query = @"SELECT [Id], [Name], [Number], [Customer], [ItemCount], [Vendor]
                                    FROM [Jobs];";

            var jobs = await _connection.QueryAsync<JobSummary>(query);

            return jobs.ToArray();

        }
    }

}