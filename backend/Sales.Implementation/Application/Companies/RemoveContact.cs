using FluentValidation;
using MediatR;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Companies;

public class RemoveContact {

    public record Command(int CompanyId, int ContactId) : IRequest;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.CompanyId)
                .NotEqual(0)
                .WithMessage("Invalid company id");

            RuleFor(x => x.ContactId)
                .NotEqual(0)
                .WithMessage("Invalid contact id");

        }

    }

    public class Handler : AsyncRequestHandler<Command> {

        private readonly CompanyRepository _repo;

        public Handler(CompanyRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            var company = await _repo.GetCompanyById(request.CompanyId);
            company.RemoveContact(request.ContactId);
            await _repo.Save(company);
        }
    }

}
