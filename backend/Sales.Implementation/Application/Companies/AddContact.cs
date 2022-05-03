using FluentValidation;
using MediatR;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Companies;

public class AddContact {

    public record Command(int CompanyId, string Name, string? Email, string? Phone) : IRequest;

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

    public class Handler : AsyncRequestHandler<Command> {

        private readonly CompanyRepository _repo;

        public Handler(CompanyRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            var company = await _repo.GetCompanyById(request.CompanyId);
            company.AddContact(new(request.Name) {
                Email = request.Email,
                Phone = request.Phone
            });
            await _repo.Save(company);
        }
    }

}
