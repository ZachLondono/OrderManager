using Dapper;
using FluentValidation;
using Manufacturing.Contracts;
using Manufacturing.Implementation.Infrastructure;
using MediatR;
using System.Data;

namespace Manufacturing.Implementation.Application;

public class CompleteJob {

    public record Command(int Id) : IRequest;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.Id)
                .NotEqual(0)
                .WithMessage("Invalid job id");

        }

    }

    public class Handler : AsyncRequestHandler<Command> {
        
        private readonly JobRepository _repo;
        private readonly IPublisher _publisher;
        private readonly IDbConnection _connection;

        public Handler(JobRepository repo, IPublisher publisher, IDbConnection connection) {
            _repo = repo;
            _publisher = publisher;
            _connection = connection;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            
            var job = await _repo.GetJobById(request.Id);
            job.Complete();
            await _repo.Save(job);

            await PublishJobCompletedNotification(request);

        }

        private async Task PublishJobCompletedNotification(Command request) {

            const string query = @"SELECT [Id] AS JobId, [Name], [Number], [Customer], [CompletedDate]
                                    FROM [Manufacturing].[Jobs]
                                    WHERE [Id] = @Id;";

            var job = await _connection.QuerySingleAsync<CompletedJob>(query, new {
                request.Id
            });

            await _publisher.Publish(new JobCompleteNotification(job));

        }

    }

}
