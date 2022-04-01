namespace Manufacturing.Contracts;

public class JobSummary {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string Number { get; set; } = string.Empty;
    
    public string? Customer { get; set; }
    
    public string? Vendor { get; set; }

    public int ItemCount { get; set; }

}