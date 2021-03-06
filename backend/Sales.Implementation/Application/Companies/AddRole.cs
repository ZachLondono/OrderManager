using FluentValidation;
using MediatR;
using Sales.Implementation.Domain;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Companies;

public class AddRole {

    public record Command(int CompanyId, CompanyRole Role) : IRequest;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.CompanyId)
                .NotEqual(0)
                .WithMessage("Invalid company id");

            RuleFor(x => x.Role)
                .NotNull()
                .NotEmpty()
                .WithMessage("Invalid company role");

        }

    }

    public class Handler : AsyncRequestHandler<Command> {

        private readonly CompanyRepository _repo;

        public Handler(CompanyRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            var company = await _repo.GetCompanyById(request.CompanyId);
            company.AddRole(request.Role);
            await _repo.Save(company);
        }
    }

}
