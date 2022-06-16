using Dapper;
using Manufacturing.Contracts;
using MediatR;
using System.Data;

namespace Manufacturing.Implementation.Application;

public class GetJobById {
    
    public record Query(int Id) : IRequest<JobDetails>;

    public class Handler : IRequestHandler<Query, JobDetails> {
        
        private readonly ManufacturingSettings _settings;

        public Handler(ManufacturingSettings settings) {
            _settings = settings;
        }

        public async Task<JobDetails> Handle(Query request, CancellationToken cancellationToken) {

            string query = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"SELECT [Id], [OrderId], [Name], [Number], [Customer], [Status], [ScheduledDate] [ReleasedDate], [CompletedDate], [ShippedDate], [ProductClass], [ProductQty], [WorkCell]
                                            FROM [Manufacturing].[Jobs]
                                            WHERE [Id] = @Id;",

                PersistanceMode.SQLite => @"SELECT [Id], [OrderId], [Name], [Number], [Customer], [Status], [ScheduledDate] [ReleasedDate], [CompletedDate], [ShippedDate], [ProductClass], [ProductQty], [WorkCell]
                                            FROM [Jobs]
                                            WHERE [Id] = @Id;",

                _ => throw new InvalidDataException("Invalid DataBase mode")

            };

            var job = await _settings.Connection.QuerySingleAsync<JobDetails>(query, new { request.Id });

            return job;

        }
    }

}