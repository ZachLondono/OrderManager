using MediatR;

namespace Manufacturing.Contracts;

public record JobReleasedNotification(string Name, string Number, string Customer, DateTime ReleaseDate) : INotification;

public record JobCompleteNotification(string Name, string Number, string Customer, DateTime CompleteDate) : INotification;

public record JobShippedNotification(string Name, string Number, string Customer, DateTime ShipDate) : INotification;