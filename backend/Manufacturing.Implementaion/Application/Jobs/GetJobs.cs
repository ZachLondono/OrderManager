using Dapper;
using Manufacturing.Contracts;
using MediatR;
using System.Data;

namespace Manufacturing.Implementation.Application;

public class GetJobs {

    public record Query() : IRequest<IEnumerable<JobSummary>>;

    public class Handler : IRequestHandler<Query, IEnumerable<JobSummary>> {

        private readonly ManufacturingSettings _settings;

        public Handler(ManufacturingSettings settings) {
            _settings = settings;
        }

        public async Task<IEnumerable<JobSummary>> Handle(Query request, CancellationToken cancellationToken) {
            
            string query = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => "SELECT [Id], [Name], [Number], [CustomerId], [VendorId], [ItemCount] FROM [Manufacturing].[Jobs];",

                PersistanceMode.SQLite => "SELECT [Id], [Name], [Number], [CustomerId], [VendorId], [ItemCount] FROM [Jobs];",

                _ => throw new InvalidDataException("Invalid DataBase mode")

            };

            var jobs = await _settings.Connection.QueryAsync<JobSummary>(query);

            return jobs;

        }
    }

}