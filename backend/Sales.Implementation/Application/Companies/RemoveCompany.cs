using MediatR;
using Dapper;
using Sales.Contracts;

namespace Sales.Implementation.Application.Companies;

public class RemoveCompany {

    public record Command(int Id) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly SalesSettings _settings;

        public Handler(SalesSettings settings) {
            _settings = settings;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {

            string command = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"DELETE FROM [Sales].[Contacts] WHERE [CompanyId] = @Id;
                                                DELETE FROM [Sales].[Companies] WHERE [Id] = @Id;",

                PersistanceMode.SQLite => @"DELETE FROM [Contacts] WHERE [CompanyId] = @Id;
                                            DELETE FROM [Companies] WHERE [Id] = @Id;",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            await _settings.Connection.ExecuteAsync(command, new {
                request.Id
            });

        }

    }

}