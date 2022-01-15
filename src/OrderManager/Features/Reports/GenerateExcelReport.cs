using ClosedXML.Report;
using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore.Features.Reports;

public class GenerateExcelReport {

    public record Command(Report Report, object ReporData, string Filename) : IRequest<ReportEnvelope> ;

    public class Validator : AbstractValidator<Command> { 
        public Validator() {
            RuleFor(c => c.Report).NotNull();
            RuleFor(c => c.Report.Template)
                .NotEmpty()
                .NotNull();

            RuleFor(c => c.ReporData).NotNull();

            RuleFor(c => c.Filename)
                .NotNull()
                .NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, ReportEnvelope> {

        private readonly IConfigurationProfile _profile;

        public Handler(IConfigurationProfile profile) {
            _profile = profile;
        }

        public Task<ReportEnvelope> Handle(Command request, CancellationToken cancellationToken) {

            string outputdir = _profile?.OutputDirectory ?? "";

            return Task.Run<ReportEnvelope>(() => {

                XLTemplate template = new(request.Report.Template);

                template.AddVariable(request.ReporData);
                template.Generate();
                template.SaveAs(outputdir);

                return new() {
                    CreationDate = DateTime.Now,
                    Output = outputdir,
                    Report = request.Report
                };

            });

        }
    }

}