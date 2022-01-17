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
                .NotNull()
                .Must(c => File.Exists(c))
                .Must(c => Path.GetExtension(c).Equals(".xlsx"))
                .WithMessage("Report template must be a valid path to an excel file");
            
            RuleFor(c => c.Report.OutputDirectory)
                .NotEmpty()
                .NotNull()
                .Must(c => Directory.Exists(c))
                .WithMessage("Report output directory must be a valid directory");
            
            RuleFor(c => c.ReporData).NotNull();
            
            RuleFor(c => c.Filename)
                .NotNull()
                .NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, ReportEnvelope> {

        public Task<ReportEnvelope> Handle(Command request, CancellationToken cancellationToken) {

            string outputdir = Path.Combine(request.Report.OutputDirectory, request.Filename);

            if (!Path.GetExtension(outputdir).Equals(".xlsx"))
                outputdir = $"{outputdir}.xlsx";

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