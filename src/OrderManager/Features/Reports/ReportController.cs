using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore.Features.Reports;

public class ReportController : BaseController {

    public ReportController(ISender sender) : base(sender) { }

    public Task<ReportEnvelope> GenerateReport(Report report, object reportData, string fileName) {
        return Sender.Send(new GenerateExcelReport.Command(report, reportData, fileName));
    }

}
