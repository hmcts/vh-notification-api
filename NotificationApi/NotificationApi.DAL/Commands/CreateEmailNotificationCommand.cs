using System;
using System.Threading.Tasks;
using NotificationApi.DAL.Commands.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Commands
{
    public class CreateEmailNotificationCommand : ICommand
    {
        public CreateEmailNotificationCommand(int notificationType, string contactEmail, Guid participantId, Guid hearingId)
        {
            NotificationType = notificationType;
            ContactEmail = contactEmail;
            ParticipantId = participantId;
            HearingId = hearingId;
        }

        public Guid NotificationId { get; set; }
        public int NotificationType { get; set; }
        public string ContactEmail { get; set; }
        public Guid ParticipantId { get; set; }
        public Guid HearingId { get; set; }
    }

    public class CreateEmailNotificationCommandHandler : ICommandHandler<CreateEmailNotificationCommand>
    {
        private readonly NotificationsApiDbContext _notificationsApiDbContext;

        public CreateEmailNotificationCommandHandler(NotificationsApiDbContext notificationsApiDbContext)
        {
            _notificationsApiDbContext = notificationsApiDbContext;
        }

        public async Task Handle(CreateEmailNotificationCommand command) 
        {
            var notification = new EmailNotification((NotificationType)command.NotificationType, command.ContactEmail, command.ParticipantId, command.HearingId);
            notification.AssignExternalId(string.Empty);
            notification.AssignPayload(string.Empty);
            await _notificationsApiDbContext.Notifications.AddAsync(notification);
            await _notificationsApiDbContext.SaveChangesAsync();

            command.NotificationId = notification.Id;
        }
    }
}
