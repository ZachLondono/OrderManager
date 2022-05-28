using Dapper;
using MediatR;
using System.Data;

namespace Catalog.Implementation.Application;

public class RemoveProductClass {

    public record Command(int ClassId) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly IDbConnection _connection;

        public Handler(IDbConnection connection) {
            _connection = connection;
        }

        
        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            const string command = @"DELETE [Catalog].[ProductClasses]
                                    WHERE [Id] = @ClassId;";

            await _connection.QuerySingleAsync<int>(command, new { request.ClassId });

        }
    }

}