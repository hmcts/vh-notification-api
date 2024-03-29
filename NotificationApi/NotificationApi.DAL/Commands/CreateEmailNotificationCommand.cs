using System;
using System.Threading.Tasks;
using NotificationApi.DAL.Commands.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Commands
{
    public class CreateEmailNotificationCommand : ICommand
    {
        public CreateEmailNotificationCommand(NotificationType notificationType, string contactEmail,
            Guid? participantId, Guid? hearingId, string parameters)
        {
            NotificationId = Guid.NewGuid();
            NotificationType = notificationType;
            ContactEmail = contactEmail;
            ParticipantId = participantId;
            HearingId = hearingId;
            Parameters = parameters;
        }

        public Guid NotificationId { get; set; }
        public NotificationType NotificationType { get; set; }
        public string ContactEmail { get; set; }
        public Guid? ParticipantId { get; set; }
        public Guid? HearingId { get; set; }
        public string Parameters { get; set; }
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
            var notification = new EmailNotification(command.NotificationId, command.NotificationType,
                command.ContactEmail, command.ParticipantId, command.HearingId);
            notification.Parameters = command.Parameters;
            _notificationsApiDbContext.Notifications.Add(notification);
            await _notificationsApiDbContext.SaveChangesAsync();
        }
    }
}
