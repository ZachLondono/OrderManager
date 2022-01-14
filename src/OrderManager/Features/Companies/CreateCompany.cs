using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Companies;

public class CreateCompany {

    public record Command(
            string CompanyName,
            string ContactName,
            string ContactEmail,
            string ContactPhone,
            string AddressLine1,
            string AddressLine2,
            string AddressLine3,
            string City,
            string State,
            string PostalCode) : IRequest<Company>;

    public class Validator : AbstractValidator<Command> {
        public Validator() {

            RuleFor(c => c.CompanyName)
                .NotEmpty()
                .NotNull();

            RuleFor(c => c.ContactName).NotNull();
            RuleFor(c => c.ContactEmail).NotNull();
            RuleFor(c => c.ContactPhone).NotNull();
            RuleFor(c => c.AddressLine1).NotNull();
            RuleFor(c => c.AddressLine2).NotNull();
            RuleFor(c => c.AddressLine3).NotNull();
            RuleFor(c => c.City).NotNull();
            RuleFor(c => c.State).NotNull();
            RuleFor(c => c.PostalCode).NotNull();

        }
    }

    public class Handler : IRequestHandler<Command, Company> {
        
        public Task<Company> Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

    }

}
