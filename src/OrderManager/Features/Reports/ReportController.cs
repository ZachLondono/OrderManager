using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore.Features.Reports;

public class ReportController : BaseController {

    public ReportController(ISender sender) : base(sender) { }

    // TODO : Add HTML reports as well, can use Razor for html templating
    public Task<ReportEnvelope> GenerateReport(Report report, object reportData, string fileName) {
        return Sender.Send(new GenerateExcelReport.Command(report, reportData, fileName));
    }

    public Task<Report> GetReportByName(string reportName) {
        return Sender.Send(new GetReportByName.Query(reportName));
    }

    public Task<IEnumerable<Report>> GetReports() {
        return Sender.Send(new GetAllReports.Query());
    }

}
