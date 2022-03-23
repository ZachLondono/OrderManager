namespace Manufacturing.Contracts;

public class JobDetails {

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public string Customer { get; set; } = string.Empty;

    public string Vendor { get; set; } = string.Empty;

    public int ItemCount { get; set; }

    public DateTime ReleaseDate { get; set; }

    public DateTime CompleteDate { get; set; }

    public DateTime ShippedDate { get; set; }

}
