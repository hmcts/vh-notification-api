namespace NotificationApi.Contract.Requests;

public class PasswordResetEmailRequest
{
    /// <summary>
    ///     The email address of the participant to send the email to
    /// </summary>
    public string ContactEmail { get; set; }

    /// <summary>
    ///     The first and last name of a participant
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The temporary password for the person
    /// </summary>
    public string Password { get; set; }
}
