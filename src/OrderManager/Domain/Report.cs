namespace OrderManager.ApplicationCore.Domain;

public enum ReportType {
    Excel,
    HTML
}

public class Report {
    
    public int ReportId { get; set; }
    
    public string ReportName { get; set; } = string.Empty;
    
    public string Template { get; set; } = string.Empty;

    public string OutputDirectory { get; set; } = string.Empty;

    public ReportType ReportType { get; set; }

}

