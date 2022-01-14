using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Reports;

public class GenerateBatchReport {

    public record Command(Report Report, List<Order> Orders, string Filename) : IRequest<ReportsEnvelope>;

    public class Validator : AbstractValidator<Command> {
        public Validator() {

        }
    }

    public class Handler : IRequestHandler<Command, ReportsEnvelope> {
        public Task<ReportsEnvelope> Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
