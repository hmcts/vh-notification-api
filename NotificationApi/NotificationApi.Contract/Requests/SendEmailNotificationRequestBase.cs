using System;

namespace NotificationApi.Contract.Requests;

public abstract class SendEmailNotificationRequestBase
{
    /// <summary>
    /// The email address of the participant to send the email to
    /// </summary>
    public string ContactEmail { get; set; }
        
    /// <summary>
    /// The UUID of the booking
    /// </summary>
    public Guid? HearingId { get; set; }
        
    /// <summary>
    /// The UUID of the participant in the booking
    /// </summary>
    public Guid? ParticipantId { get; set; }
}
