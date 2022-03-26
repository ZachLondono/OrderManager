using MediatR;

namespace Manufacturing.Contracts;

public record JobReleasedNotification(Guid JobId, string Name, string Number, string Customer, DateTime ReleaseDate) : INotification;

public record JobCompleteNotification(Guid JobId, string Name, string Number, string Customer, DateTime CompleteDate) : INotification;

public record JobShippedNotification(Guid JobId, string Name, string Number, string Customer, DateTime ShipDate) : INotification;