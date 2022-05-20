namespace Manufacturing.Contracts;

public class JobDetails {

    public int Id { get; set; }

    public int OrderId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public string Customer { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime? ScheduledDate { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public DateTime? CompleteDate { get; set; }

    public DateTime? ShipDate { get; set; }

    public Dictionary<int, int> Products { get; set; } = new();

}