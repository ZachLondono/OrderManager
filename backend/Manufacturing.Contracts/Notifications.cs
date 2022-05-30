using MediatR;

namespace Manufacturing.Contracts;

public record JobCompleteNotification(CompletedJob Job) : INotification;

public record CompletedJob {
    public int JobId { get; set; }
    public int OrderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Customer { get; set; } = string.Empty;
    public DateTime CompletedDate { get; set; }
}

public record JobShippedNotification(ShippedJob Job) : INotification;

public record ShippedJob {
    public int JobId { get; set; }
    public int OrderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Customer { get; set; } = string.Empty;
    public DateTime ShippedDate { get; set; }
}