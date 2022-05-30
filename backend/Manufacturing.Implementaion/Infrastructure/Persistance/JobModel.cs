namespace Manufacturing.Implementation.Infrastructure.Persistance;

internal class JobModel {

    public int Id { get; set; }

    public int OrderId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Number { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public DateTime? ScheduledDate { get; set; }

    public DateTime? ReleasedDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public int ProductClass { get; set; }

    public int ProductQty { get; set; }

    public int? WorkCell { get; set; } = null;

}