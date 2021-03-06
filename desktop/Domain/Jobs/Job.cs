namespace OrderManager.Domain.Jobs;

public class Job {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public string? Customer { get; set; }

    public string? Vendor { get; set; }

    public int ItemCount { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public DateTime? CompleteDate { get; set; }

    public DateTime? ShipDate { get; set; }

}