namespace NotificationApi.Contract.Requests;

public class HearingAmendmentRequest
{
    /// <summary>
    ///     The email address of the participant to send the email to
    /// </summary>
    public string ContactEmail { get; set; }

    /// <summary>
    ///     The UUID of the booking
    /// </summary>
    public Guid? HearingId { get; set; }

    /// <summary>
    ///     The UUID of the participant in the booking
    /// </summary>
    public Guid? ParticipantId { get; set; }

    /// <summary>
    ///     The first and last name of a participant
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The display name of a participant
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    ///     The user role
    /// </summary>
    public string RoleName { get; set; }

    /// <summary>
    ///     The hearing case number
    /// </summary>
    public string CaseNumber { get; set; }

    /// <summary>
    ///     The hearing case name
    /// </summary>
    public string CaseName { get; set; }

    /// <summary>
    ///     The previous hearing scheduled date and time
    /// </summary>
    public DateTime PreviousScheduledDateTime { get; set; }
    
    /// <summary>
    ///     The new hearing scheduled date and time
    /// </summary>
    public DateTime NewScheduledDateTime { get; set; }

    /// <summary>
    ///     The existing participant's username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    ///     The name of the person the participant is representing (if applicable)
    /// </summary>
    public string Representee { get; set; }
}
