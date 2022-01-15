using MediatR;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Reports;

public class ReportController {

    private readonly ISender _sender;
    public ReportController(ISender sender) {
        _sender = sender;
    }

    public Task<ReportEnvelope> GenerateReport(Report report, object reportData, string fileName) {
        return _sender.Send(new GenerateExcelReport.Command(report, reportData, fileName));
    }

}
