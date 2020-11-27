using System;
using System.Threading.Tasks;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Queries
{
    public class CreateEmailNotificationQuery : ICommand
    {
        public CreateEmailNotificationQuery(int notificationType, string contactEmail, Guid participantId, Guid hearingId)
        {
            NotificationType = notificationType;
            ContactEmail = contactEmail;
            ParticipantId = participantId;
            HearingId = hearingId;
        }

        public int NotificationType { get; set; }
        public string ContactEmail { get; set; }
        public Guid ParticipantId { get; set; }
        public Guid HearingId { get; set; }
    }

    public class CreateEmailNotificationCommandHandler : IQueryHandler<CreateEmailNotificationQuery, Notification>
    {
        private readonly NotificationsApiDbContext _notificationsApiDbContext;

        public CreateEmailNotificationCommandHandler(NotificationsApiDbContext notificationsApiDbContext)
        {
            _notificationsApiDbContext = notificationsApiDbContext;
        }

        public async Task<Notification> Handle(CreateEmailNotificationQuery query) 
        {
            var notification = new EmailNotification((NotificationType)query.NotificationType, query.ContactEmail, query.ParticipantId, query.HearingId);
            _notificationsApiDbContext.Notifications.Add(notification);
            await _notificationsApiDbContext.SaveChangesAsync();

            return notification;
        }
    }
}
