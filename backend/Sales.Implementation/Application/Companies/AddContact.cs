using Dapper;
using FluentValidation;
using MediatR;
using Sales.Implementation.Infrastructure;
using System.Data;
using System.Linq;

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
        private readonly IDbConnection _connection;

        public Handler(CompanyRepository repo, IDbConnection connection) {
            _repo = repo;
            _connection = connection;
        }

        public async Task<int> Handle(Command request, CancellationToken cancellationToken) {

            var company = await _repo.GetCompanyById(request.CompanyId);
            company.AddContact(new(request.Name) {
                Email = request.Email,
                Phone = request.Phone
            });
            await _repo.Save(company);

            const string query = @"SELECT [Id]
                                    FROM [Sales].[Contacts]
                                    WHERE [CompanyId] = @CompanyId And [Name] = @Name;";

            IEnumerable<int> newId = await _connection.QueryAsync<int>(query, new {
                CompanyId = company.Id,
                request.Name
            });

            return newId.OrderByDescending(i => i).FirstOrDefault();
        }
    }

}