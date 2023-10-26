namespace NotificationApi.Contract.Requests
{
    public class NewUserWelcomeEmailRequest : SendEmailNotificationRequestBase
    {
        /// <summary>
        /// The first and last name of a participant
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The hearing case number
        /// </summary>
        public string CaseNumber { get; set; }
    
        /// <summary>
        /// The hearing case name
        /// </summary>
        public string CaseName { get; set; }

        /// <summary>
        /// The user role
        /// </summary>
        public string RoleName { get; set; }
    }

    public static class RoleNames
    {
        public const string PanelMember = "PanelMember";
        public const string Winger = "Winger";
        public const string Judge = "Judge";
        public const string JudicialOfficeHolder = "Judicial Office Holder";
        public const string Representative = "Representative";
        public const string Individual = "Individual";
    }
}
