using MediatR;
using System.Data;
using Dapper;

namespace Sales.Implementation.Application.Companies;

public class RemoveCompany {

    public record Command(int Id) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            const string command = @"DELETE FROM [Sales].[Companies] WHERE [Id] = @Id;";

            await _connection.ExecuteAsync(command, new {
                request.Id
            });

        }

    }

}