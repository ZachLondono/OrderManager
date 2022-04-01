using MediatR;

namespace Manufacturing.Contracts;

public record JobReleasedNotification(int JobId, string Name, string Number, string Customer, DateTime ReleaseDate) : INotification;

public record JobCompleteNotification(int JobId, string Name, string Number, string Customer, DateTime CompleteDate) : INotification;

public record JobShippedNotification(int JobId, string Name, string Number, string Customer, DateTime ShipDate) : INotification;