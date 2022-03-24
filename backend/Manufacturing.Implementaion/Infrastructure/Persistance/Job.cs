namespace Manufacturing.Implementaion.Infrastructure.Persistance;

internal class Job {

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public string Customer { get; set; } = string.Empty;

    public string Vendor { get; set; } = string.Empty;

    public int Items { get; set; }

    public DateTime ReleaseDate { get; set; }

    public DateTime CompleteDate { get; set; }

    public DateTime ShipDate { get; set; }

    public string Status { get; set; } = string.Empty;

}
