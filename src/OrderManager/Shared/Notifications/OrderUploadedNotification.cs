using MediatR;

namespace OrderManager.Shared.Notifications;

public record OrderUploadedNotification(int Id) : INotification;
