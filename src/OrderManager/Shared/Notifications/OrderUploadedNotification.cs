using MediatR;
using System;

namespace OrderManager.Shared.Notifications;

public record OrderUploadedNotification(Guid Id) : INotification;
