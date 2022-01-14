using ClosedXML.Report;
using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Reports;
public class GenerateReport {

    public record Command(Report Report, Order Order, string Filename) : IRequest<ReportEnvelope> ;

    public class Validator : AbstractValidator<Command> { 
        public Validator() {
            RuleFor(c => c.Report).NotNull();
            RuleFor(c => c.Report.Template)
                .NotEmpty()
                .NotNull();

            RuleFor(c => c.Order).NotNull();

            RuleFor(c => c.Filename)
                .NotNull()
                .NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, ReportEnvelope> {
        public Task<ReportEnvelope> Handle(Command request, CancellationToken cancellationToken) {

            // Get outputfile from profile
            const string outputFile = "";

            return Task.Run<ReportEnvelope>(() => {

                XLTemplate template = new(request.Report.Template);

                template.AddVariable(request.Order);
                template.Generate();
                template.SaveAs(outputFile);

                return new() {
                    CreationDate = DateTime.Now,
                    Output = outputFile,
                    Report = request.Report
                };

            });

        }
    }

}