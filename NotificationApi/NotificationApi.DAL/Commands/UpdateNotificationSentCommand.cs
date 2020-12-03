using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationApi.DAL.Commands.Core;
using NotificationApi.DAL.Exceptions;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Commands
{
    public class UpdateNotificationSentCommand : ICommand
    {
        public UpdateNotificationSentCommand(Guid notificationId, string externalId, string payload)
        {
            NotificationId = notificationId;
            ExternalId = externalId;
            Payload = payload;
        }

        public Guid NotificationId { get; set; }
        public string ExternalId { get; set; }
        public string Payload { get; set; }
    }

    public class UpdateNotificationSentCommandHandler : ICommandHandler<UpdateNotificationSentCommand>
    {
        private readonly NotificationsApiDbContext _notificationsApiDbContext;

        public UpdateNotificationSentCommandHandler(NotificationsApiDbContext notificationsApiDbContext)
        {
            _notificationsApiDbContext = notificationsApiDbContext;
        }

        public async Task Handle(UpdateNotificationSentCommand command)
        {
            var notification = await _notificationsApiDbContext.Notifications.SingleOrDefaultAsync(x => x.Id == command.NotificationId);
            if (notification == null)
            {
                throw new NotificationNotFoundException(command.NotificationId);
            }
            
            notification.AssignPayload(command.Payload);
            notification.AssignExternalId(command.ExternalId);

            notification.UpdateDeliveryStatus(DeliveryStatus.Created);

            await _notificationsApiDbContext.SaveChangesAsync();
        }
    }
}
