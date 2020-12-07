using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationApi.DAL.Commands.Core;
using NotificationApi.DAL.Exceptions;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Commands
{
    public class UpdateNotificationDeliveryStatusCommand : ICommand
    {
        public UpdateNotificationDeliveryStatusCommand(Guid notificationId, string externalId,
            DeliveryStatus deliveryStatus)
        {
            NotificationId = notificationId;
            ExternalId = externalId;
            DeliveryStatus = deliveryStatus;
        }

        public Guid NotificationId { get; }
        public string ExternalId { get; }
        public DeliveryStatus DeliveryStatus { get; }
    }

    public class
        UpdateNotificationDeliveryStatusCommandHandler : ICommandHandler<UpdateNotificationDeliveryStatusCommand>
    {
        private readonly NotificationsApiDbContext _notificationsApiDbContext;

        public UpdateNotificationDeliveryStatusCommandHandler(NotificationsApiDbContext notificationsApiDbContext)
        {
            _notificationsApiDbContext = notificationsApiDbContext;
        }

        public async Task Handle(UpdateNotificationDeliveryStatusCommand command)
        {
            var notification =
                await _notificationsApiDbContext.Notifications.SingleOrDefaultAsync(x =>
                    x.Id == command.NotificationId);

            if (notification == null)
            {
                throw new NotificationNotFoundException(command.NotificationId);
            }

            if (notification.ExternalId != command.ExternalId)
            {
                throw new NotificationIdMismatchException(command.NotificationId, command.ExternalId);
            }

            notification.UpdateDeliveryStatus(command.DeliveryStatus);

            await _notificationsApiDbContext.SaveChangesAsync();
        }
    }
}
