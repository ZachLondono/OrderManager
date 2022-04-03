using Dapper;
using Manufacturing.Implementation.Domain;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Manufacturing.Implementation.Infrastructure;

public class JobRepository {

    private readonly IDbConnection _connection;
    private readonly ILogger<JobRepository> _logger;

    public JobRepository(IDbConnection connection, ILogger<JobRepository> logger) {
        _connection = connection;
        _logger = logger;
    }

    public async Task<JobContext> GetJobById(int jobId) {

        const string query = @"SELECT [Id], [Name], [Number], [CustomerId], [VendorId], [ItemCount], [ReleasedDate], [CompletedDate], [ShippedDate], [Status]
                                FROM [Manufacturing].[Jobs]
                                WHERE [Id] = @Id;";

        var job = await _connection.QuerySingleAsync<Persistance.Job>(query, new { Id = jobId });

        ManufacturingStatus status = (ManufacturingStatus) Enum.Parse(typeof(ManufacturingStatus), job.Status);

        _logger.LogInformation("Found job with ID {ID}, {Job}", job);

        return new(new Job(job.Id,
                            job.Name,
                            job.Number, 
                            job.CustomerId,
                            job.VendorId,
                            job.ItemCount,
                            job.ReleasedDate,
                            job.CompletedDate,
                            job.ShippedDate,
                            status));

    }

    public async Task Save(JobContext context) {
        
        _connection.Open();
        var trx = _connection.BeginTransaction();
        var events = context.Events;

        foreach (var e in events) {

            if (e is JobCanceledEvent) {
                await ApplyCancel(context, trx);
            } else if (e is JobReleasedEvent releaseEvent) {
                await ApplyRelease(context, releaseEvent, trx);
            } else if (e is JobCompletedEvent completeEvent) {
                await ApplyComplete(context, completeEvent, trx);
            } else if (e is JobShippedEvent shipEvent) {
                await ApplyShip(context, shipEvent, trx);
            } 

        }

        trx.Commit();
        _connection.Close();

        _logger.LogInformation("Applied {EventCount} events to job with Id {ID}", events.Count, context.Id);

    }

    private async Task ApplyCancel(JobContext context, IDbTransaction trx) {
        const string command = "UPDATE [Manufacturing].[Jobs] SET [Status] = @Status WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            Status = ManufacturingStatus.Canceled.ToString(),
            context.Id
        }, trx);
    }

    private async Task ApplyRelease(JobContext context, JobReleasedEvent releaseEvent, IDbTransaction trx) {
        const string command = "UPDATE [Manufacturing].[Jobs] SET [Status] = @Status, [ReleasedDate] = @Date WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            Status = ManufacturingStatus.InProgress.ToString(),
            context.Id,
            Date = releaseEvent.ReleaseTimestamp
        }, trx);
    }

    private async Task ApplyComplete(JobContext context, JobCompletedEvent completeEvent, IDbTransaction trx) {
        const string command = "UPDATE [Manufacturing].[Jobs] SET [Status] = @Status, [CompletedDate] = @Date WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            Status = ManufacturingStatus.Completed.ToString(),
            context.Id,
            Date = completeEvent.CompleteTimestamp
        }, trx);
    }

    private async Task ApplyShip(JobContext context, JobShippedEvent shipEvent, IDbTransaction trx) {
        const string command = "UPDATE [Manufacturing].[Jobs] SET [Status] = @Status, [ShippedDate] = @Date WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            Status = ManufacturingStatus.Shipped.ToString(),
            context.Id,
            Date = shipEvent.ShipTimestamp
        }, trx);
    }

}
