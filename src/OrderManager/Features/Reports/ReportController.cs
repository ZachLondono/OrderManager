using MediatR;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Reports;

public class ReportController {

    private readonly ISender _sender;
    public ReportController(ISender sender) {
        _sender = sender;
    }

    public Task<ReportEnvelope> GenerateReport(Report report, Order order, string fileName) {
        return _sender.Send(new GenerateReport.Command(report, order, fileName));
    }

    public Task<ReportsEnvelope> GenerateReport(Report report, List<Order> orders, string fileName) {
        return _sender.Send(new GenerateBatchReport.Command(report, orders, fileName));
    }

}
