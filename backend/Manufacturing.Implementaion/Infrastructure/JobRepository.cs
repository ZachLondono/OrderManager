using Dapper;
using Manufacturing.Implementation.Domain;
using System.Data;

namespace Manufacturing.Implementation.Infrastructure;

public class JobRepository {

    private readonly IDbConnection _connection;

    public JobRepository(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<JobContext> GetJobById(int jobId) {

        const string query = @"SELECT [Id], [Name], [Number], [CustomerId], [VendorId], [ItemCount], [ReleasedDate], [CompletedDate], [ShippedDate], [Status]
                                FROM [Manufacturing].[Jobs]
                                WHERE [Id] = @Id;";

        var job = await _connection.QuerySingleAsync<Persistance.Job>(query, new { Id = jobId });

        ManufacturingStatus status = (ManufacturingStatus) Enum.Parse(typeof(ManufacturingStatus), job.Status);

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

    }

    private async Task ApplyCancel(JobContext context, IDbTransaction trx) {
        const string command = "UPDATE [Manufacturing].[Jobs] SET [Status] = @Status WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            Status = ManufacturingStatus.Canceled.ToString(),
            Id = context.Id
        }, trx);
    }

    private async Task ApplyRelease(JobContext context, JobReleasedEvent releaseEvent, IDbTransaction trx) {
        const string command = "UPDATE [Manufacturing].[Jobs] SET [Status] = @Status, [ReleasedDate] = @Date WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            Status = ManufacturingStatus.InProgress.ToString(),
            Id = context.Id,
            Date = releaseEvent.ReleaseTimestamp
        }, trx);
    }

    private async Task ApplyComplete(JobContext context, JobCompletedEvent completeEvent, IDbTransaction trx) {
        const string command = "UPDATE [Manufacturing].[Jobs] SET [Status] = @Status, [CompletedDate] = @Date WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            Status = ManufacturingStatus.Completed.ToString(),
            Id = context.Id,
            Date = completeEvent.CompleteTimestamp
        }, trx);
    }

    private async Task ApplyShip(JobContext context, JobShippedEvent shipEvent, IDbTransaction trx) {
        const string command = "UPDATE [Manufacturing].[Jobs] SET [Status] = @Status, [ShippedDate] = @Date WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            Status = ManufacturingStatus.Shipped.ToString(),
            Id = context.Id,
            Date = shipEvent.ShipTimestamp
        }, trx);
    }

}
