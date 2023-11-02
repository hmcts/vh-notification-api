namespace NotificationApi.Contract.Requests;

public class SignInDetailsEmailRequest
{
    /// <summary>
    ///     The email address of the person to send the email to
    /// </summary>
    public string ContactEmail { get; set; }

    /// <summary>
    ///     The first and last name of a person
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     The new username for the person
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    ///     The temporary password for the person
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    ///     The role name of the person
    /// </summary>
    public string RoleName { get; set; }
}
