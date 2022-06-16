using Dapper;
using Manufacturing.Contracts;
using Manufacturing.Implementation.Domain;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Manufacturing.Implementation.Infrastructure;

public class JobRepository {

    private readonly ManufacturingSettings _settings;
    private readonly ILogger<JobRepository> _logger;

    public JobRepository(ManufacturingSettings settings, ILogger<JobRepository> logger) {
        _settings = settings;
        _logger = logger;
    }

    public async Task<JobContext> GetJobById(int jobId) {

        string query = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"SELECT [Id], [OrderId], [Name], [Number], [Customer], [CustomerName], [ScheduledDate], [ReleasedDate], [CompletedDate], [ShippedDate], [Status], [ProductClass], [ProductQty], [WorkCell]
                                            FROM [Manufacturing].[Jobs]
                                            WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"SELECT [Id], [OrderId], [Name], [Number], [Customer], [CustomerName], [ScheduledDate], [ReleasedDate], [CompletedDate], [ShippedDate], [Status], [ProductClass], [ProductQty], [WorkCell]
                                        FROM [Jobs]
                                        WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid DataBase mode")

        };

        var job = await _settings.Connection.QuerySingleAsync<Persistance.JobModel>(query, new { Id = jobId });

        ManufacturingStatus status = (ManufacturingStatus) Enum.Parse(typeof(ManufacturingStatus), job.Status);

        _logger.LogInformation("Found job with ID {ID}, {Job}", job);

        return new(new Job(job.Id,
                            job.OrderId,
                            job.Name,
                            job.Number,
                            job.CustomerName,
                            job.ScheduledDate,
                            job.ReleasedDate,
                            job.CompletedDate,
                            job.ShippedDate,
                            status,
                            job.ProductClass,
                            job.ProductQty,
                            job.WorkCell));

    }

    public async Task Save(JobContext context) {

        if (_settings.Connection is null) return;

        _settings.Connection.Open();
        var trx = _settings.Connection.BeginTransaction();
        var events = context.Events;

        foreach (var e in events) {

            if (e is JobCanceledEvent) {
                await ApplyCancel(context, trx);
            } else if (e is JobCompletedEvent completeEvent) {
                await ApplyComplete(context, completeEvent, trx);
            } else if (e is JobShippedEvent shipEvent) {
                await ApplyShip(context, shipEvent, trx);
            } 

        }

        trx.Commit();
        _settings.Connection.Close();

        _logger.LogInformation("Applied {EventCount} events to job with Id {ID}", events.Count, context.Id);

    }

    private async Task ApplyCancel(JobContext context, IDbTransaction trx) {
        
        string command = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => "UPDATE [Manufacturing].[Jobs] SET [Status] = @Status WHERE [Id] = @Id;",

            PersistanceMode.SQLite => "UPDATE [Jobs] SET [Status] = @Status WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid DataBase mode")

        };

        await _settings.Connection.ExecuteAsync(command, new {
            Status = ManufacturingStatus.Canceled.ToString(),
            context.Id
        }, trx);
    }

    private async Task ApplyComplete(JobContext context, JobCompletedEvent completeEvent, IDbTransaction trx) {
        
        string command = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => "UPDATE [Manufacturing].[Jobs] SET [Status] = @Status, [CompletedDate] = @Date WHERE [Id] = @Id;",

            PersistanceMode.SQLite => "UPDATE [Jobs] SET [Status] = @Status, [CompletedDate] = @Date WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid DataBase mode")

        };

        await _settings.Connection.ExecuteAsync(command, new {
            Status = ManufacturingStatus.Completed.ToString(),
            context.Id,
            Date = completeEvent.CompleteTimestamp
        }, trx);
    }

    private async Task ApplyShip(JobContext context, JobShippedEvent shipEvent, IDbTransaction trx) {
        
        string command = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => "UPDATE [Manufacturing].[Jobs] SET [Status] = @Status, [ShippedDate] = @Date WHERE [Id] = @Id;",

            PersistanceMode.SQLite => "UPDATE [Jobs] SET [Status] = @Status, [ShippedDate] = @Date WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid DataBase mode")

        };

        await _settings.Connection.ExecuteAsync(command, new {
            Status = ManufacturingStatus.Shipped.ToString(),
            context.Id,
            Date = shipEvent.ShipTimestamp
        }, trx);
    }

}
