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
        private readonly ManufacturingSettings _settings;

        public Handler(JobRepository repo, IPublisher publisher, ManufacturingSettings settings) {
            _repo = repo;
            _publisher = publisher;
            _settings = settings;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            
            var job = await _repo.GetJobById(request.Id);
            job.Complete();
            await _repo.Save(job);

            await PublishJobCompletedNotification(request);

        }

        private async Task PublishJobCompletedNotification(Command request) {

            string query = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"SELECT [Id] AS JobId, [OrderId], [Name], [Number], [Customer], [CompletedDate]
                                                FROM [Manufacturing].[Jobs]
                                                WHERE [Id] = @Id;",

                PersistanceMode.SQLite => @"SELECT [Id] AS JobId, [OrderId], [Name], [Number], [Customer], [CompletedDate]
                                            FROM [Jobs]
                                            WHERE [Id] = @Id;",

                _ => throw new InvalidDataException("Invalid DataBase mode")

            };

            var job = await _settings.Connection.QuerySingleAsync<CompletedJob>(query, new {
                request.Id
            });

            await _publisher.Publish(new JobCompleteNotification(job));

        }

    }

}
