using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Reports;

public class ReportEnvelope {
    public DateTime CreationDate { get; set; }
    public string? Output { get; set; }
    public Report? Report { get; set; }

}
