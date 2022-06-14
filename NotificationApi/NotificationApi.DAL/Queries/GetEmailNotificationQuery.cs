using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;

namespace NotificationApi.DAL.Queries
{
    public class GetEmailNotificationQuery : IQuery
    {
        public NotificationType NotificationType { get; }
        public Guid? HearingRefId { get; }
        public Guid? ParticipantRefId { get; }
        public string ToEmail { get; }

        public GetEmailNotificationQuery(Guid? hearingRefId, Guid? participantRefId, NotificationType notificationType, string toEmail)
        {
            HearingRefId = hearingRefId;
            ParticipantRefId = participantRefId;
            NotificationType = notificationType;
            ToEmail = toEmail;
        }
    }

    public class GetEmailNotificationQueryHandler : IQueryHandler<GetEmailNotificationQuery, EmailNotification>
    {
        private readonly NotificationsApiDbContext _notificationsApiDbContext;

        public GetEmailNotificationQueryHandler(NotificationsApiDbContext notificationsApiDbContext)
        {
            _notificationsApiDbContext = notificationsApiDbContext;
        }

        public Task<EmailNotification> Handle(GetEmailNotificationQuery query) =>
            _notificationsApiDbContext.Notifications.OfType<EmailNotification>().SingleOrDefaultAsync(t =>
                t.NotificationType == query.NotificationType &&
                t.HearingRefId == query.HearingRefId &&
                t.ParticipantRefId == query.ParticipantRefId &&
                t.ToEmail.ToLower().Trim() ==query.ToEmail.Trim());
    }
}
