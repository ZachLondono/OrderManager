using Dapper;
using FluentValidation;
using MediatR;
using Sales.Contracts;
using Sales.Implementation.Infrastructure;
using System.Data;

namespace Sales.Implementation.Application.Companies;

public class AddContact {

    public record Command(int CompanyId, string Name, string? Email, string? Phone) : IRequest<int>;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.CompanyId)
                .NotEqual(0)
                .WithMessage("Invalid company id");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Invalid company name");

        }

    }

    public class Handler : IRequestHandler<Command,int> {

        private readonly CompanyRepository _repo;
        private readonly SalesSettings _settings;

        public Handler(CompanyRepository repo, SalesSettings settings) {
            _repo = repo;
            _settings = settings;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) {

            var company = await _repo.GetCompanyById(request.CompanyId);
            company.AddContact(new(request.Name) {
                Email = request.Email,
                Phone = request.Phone
            });
            await _repo.Save(company);

            string query = _settings.PersistanceMode switch {

                PersistanceMode.SQLServer => @"SELECT [Id]
                                            FROM [Sales].[Contacts]
                                            WHERE [CompanyId] = @CompanyId And [Name] = @Name;",

                PersistanceMode.SQLite => @"SELECT [Id]
                                            FROM [Sales].[Contacts]
                                            WHERE [CompanyId] = @CompanyId And [Name] = @Name;",

                _ => throw new InvalidDataException("Invalid persistance mode")

            };

            IEnumerable<int> newId = await _settings.Connection.QueryAsync<int>(query, new {
                CompanyId = company.Id,
                request.Name
            });

            return newId.OrderByDescending(i => i).FirstOrDefault();
        }
    }

}