namespace Manufacturing.Implementation.Infrastructure.Persistance;

internal class Job {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public int CustomerId { get; set; }

    public int VendorId { get; set; }

    public int ItemCount { get; set; }

    public DateTime? ReleasedDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public string Status { get; set; } = string.Empty;

}
