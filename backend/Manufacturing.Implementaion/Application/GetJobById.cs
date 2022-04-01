using Dapper;
using Manufacturing.Contracts;
using MediatR;
using System.Data;

namespace Manufacturing.Implementation.Application;

internal class GetJobById {
    
    public record Query(int Id) : IRequest<JobDetails>;

    public class Handler : IRequestHandler<Query, JobDetails> {
        
        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        public async Task<JobDetails> Handle(Query request, CancellationToken cancellationToken) {

            const string query = @"SELECT [Id], [Name], [Number], [Customer], [ItemCount], [Vendor], [ReleaseDate], [CompleteDate], [ShipDate]
                                    FROM [Jobs]
                                    WHERE [Id] = @Id;";

            var job = await _connection.QuerySingleAsync<JobDetails>(query, new { request.Id });

            return job;

        }
    }

}