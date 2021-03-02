using Microsoft.EntityFrameworkCore;
using NotificationApi.DAL.Queries.Core;
using NotificationApi.Domain;
using NotificationApi.Domain.Enums;
using System.Threading.Tasks;

namespace NotificationApi.DAL.Queries
{
    public class GetNotificationByParticipantAndHearingQuery : IQuery
    {
        public NotificationType NotificationType { get; }
        public string ParticipantId { get; }
        public string HearingId { get; }
        public GetNotificationByParticipantAndHearingQuery(NotificationType notificationType, string heairngId, string participantId)
        {
            NotificationType = notificationType;
            HearingId = heairngId;
            ParticipantId = participantId;
        }
    }

    public class GetNotificationByParticipantAndHearingQueryHandler : IQueryHandler<GetNotificationByParticipantAndHearingQuery, EmailNotification>
    {
        private readonly NotificationsApiDbContext _notificationsApiDbContext;

        public GetNotificationByParticipantAndHearingQueryHandler(NotificationsApiDbContext notificationsApiDbContext)
        {
            _notificationsApiDbContext = notificationsApiDbContext;
        }

        public Task<EmailNotification> Handle(GetNotificationByParticipantAndHearingQuery query) =>  
            _notificationsApiDbContext.EmailNotifications.FirstOrDefaultAsync(t => t.NotificationType == query.NotificationType
                                                                        && t.HearingRefId.ToString() == query.HearingId
                                                                        && t.ParticipantRefId.ToString() == query.ParticipantId);
    }
}
