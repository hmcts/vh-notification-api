namespace NotificationApi.Services;

public static class NotificationParameterMapper
{
    public static Dictionary<string, string> MapToV1AccountCreated(SignInDetailsEmailRequest request)
    {
        return new Dictionary<string, string>
        {
            {NotifyParams.Name, request.Name},
            {NotifyParams.UserName, request.Username.ToLower()},
            {NotifyParams.RandomPassword, request.Password}
        };
    }
    
    public static Dictionary<string, string> MapToPasswordReset(PasswordResetEmailRequest request)
    {
        return new Dictionary<string, string>
        {
            {NotifyParams.Name, request.Name},
            {NotifyParams.Password, request.Password}
        };
    }
    
    public static Dictionary<string, string> MapToWelcomeEmail(NewUserWelcomeEmailRequest request)
    {
        return new Dictionary<string, string>
        {
            {NotifyParams.Name, request.Name},
            {NotifyParams.CaseName, request.CaseName},
            {NotifyParams.CaseNumber, request.CaseNumber}
        };
    }

    public static Dictionary<string, string> MapToSingleDayConfirmationNewUser(
        NewUserSingleDayHearingConfirmationRequest request)
    {
        return new Dictionary<string, string>
        {
            {NotifyParams.CaseName, request.CaseName},
            {NotifyParams.CaseNumber, request.CaseNumber},
            {NotifyParams.Time, request.ScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.DayMonthYear, request.ScheduledDateTime.ToEmailDateGbLocale()},
            {NotifyParams.DayMonthYearCy, request.ScheduledDateTime.ToEmailDateCyLocale()},
            {NotifyParams.Name, request.Name},
            {NotifyParams.StartTime, request.ScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.UserName, request.Username.ToLower()},
            {NotifyParams.RandomPassword, request.RandomPassword}
        };
    }
    
    public static Dictionary<string, string> MapToMultiDayConfirmationNewUser(NewUserMultiDayHearingConfirmationRequest request)
    {
        var parameters = new Dictionary<string, string>
        {
            {NotifyParams.CaseName, request.CaseName},
            {NotifyParams.CaseNumber, request.CaseNumber},
            {NotifyParams.Time, request.ScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.DayMonthYear, request.ScheduledDateTime.ToEmailDateGbLocale()},
            {NotifyParams.DayMonthYearCy, request.ScheduledDateTime.ToEmailDateCyLocale()},
            {NotifyParams.Name, request.Name},
            {NotifyParams.StartTime, request.ScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.UserName, request.Username.ToLower()},
            {NotifyParams.RandomPassword, request.RandomPassword},
            {NotifyParams.TotalDays, request.TotalDays.ToString()}
        };
        return parameters;
    }
    
    public static Dictionary<string, string> MapToSingleDayConfirmationExistingUser(
        ExistingUserSingleDayHearingConfirmationRequest request)
    {
        var parameters = new Dictionary<string, string>
        {
            {NotifyParams.Name, request.Name},
            {NotifyParams.CaseName, request.CaseName},
            {NotifyParams.CaseNumber, request.CaseNumber},
            {NotifyParams.DayMonthYear, request.ScheduledDateTime.ToEmailDateGbLocale()},
            {NotifyParams.Time, request.ScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.StartTime, request.ScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.UserName, request.Username.ToLower()},
            {NotifyParams.DayMonthYearCy, request.ScheduledDateTime.ToEmailDateCyLocale()}
        };
        
        if (request.RoleName == RoleNames.Judge)
        {
            parameters.Add(NotifyParams.Judge, request.DisplayName);
            parameters.Add(NotifyParams.CourtroomAccountUserName, request.Username);
        }

        if (request.RoleName == RoleNames.JudicialOfficeHolder)
        {
            parameters.Add(NotifyParams.JudicialOfficeHolder, request.Name);
        }

        if (request.RoleName == RoleNames.Representative)
        {
            parameters.Add(NotifyParams.ClientName, request.Representee);
            parameters.Add(NotifyParams.SolicitorName, request.Name);
        }
        
        return parameters;
    }
    
    public static Dictionary<string, string> MapToMultiDayConfirmationForExistingUser(
        ExistingUserMultiDayHearingConfirmationRequest request)
    {
        var parameters = new Dictionary<string, string>
        {
            {NotifyParams.CaseName, request.CaseName},
            {NotifyParams.CaseNumber, request.CaseNumber},
            {NotifyParams.Time, request.ScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.StartDayMonthYear, request.ScheduledDateTime.ToEmailDateGbLocale()},
            {NotifyParams.DayMonthYear, request.ScheduledDateTime.ToEmailDateGbLocale()},
            {NotifyParams.DayMonthYearCy, request.ScheduledDateTime.ToEmailDateCyLocale()},
            {NotifyParams.Name, request.Name},
            {NotifyParams.StartTime, request.ScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.UserName, request.Username.ToLower()},
            {NotifyParams.TotalDays, request.TotalDays.ToString()},
        };
        
        if (request.RoleName == RoleNames.Judge)
        {
            parameters.Add(NotifyParams.Judge, request.DisplayName);
            parameters.Add(NotifyParams.CourtroomAccountUserName, request.Username.ToLower());
        }

        if (request.RoleName == RoleNames.JudicialOfficeHolder)
        {
            parameters.Add(NotifyParams.JudicialOfficeHolder, request.Name);
        }

        if (request.RoleName == RoleNames.Representative)
        {
            parameters.Add(NotifyParams.ClientName, request.Representee);
            parameters.Add(NotifyParams.SolicitorName, request.Name);
        }
        
        return parameters;
    }
    
    public static Dictionary<string, string> MapToSingleDayReminder(SingleDayHearingReminderRequest request)
    {
        var parameters = new Dictionary<string, string>
        {
            {NotifyParams.CaseName, request.CaseName},
            {NotifyParams.CaseNumber, request.CaseNumber},
            {NotifyParams.Time, request.ScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.DayMonthYear, request.ScheduledDateTime.ToEmailDateGbLocale()},
            {NotifyParams.DayMonthYearCy, request.ScheduledDateTime.ToEmailDateCyLocale()},
            {NotifyParams.Name, request.Name},
            {NotifyParams.StartTime, request.ScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.UserName, request.Username.ToLower()},
        };
        
        if (request.RoleName == RoleNames.JudicialOfficeHolder)
        {
            parameters.Add(NotifyParams.JudicialOfficeHolder, request.Name);
        }

        if (request.RoleName == RoleNames.Representative)
        {
            parameters.Add(NotifyParams.ClientName, request.Representee);
            parameters.Add(NotifyParams.SolicitorName, request.Name);
        }
        return parameters;
    }
    
    public static Dictionary<string, string> MapToMultiDayReminder(MultiDayHearingReminderRequest request)
    {
        var parameters = new Dictionary<string, string>
        {
            {NotifyParams.CaseName, request.CaseName},
            {NotifyParams.CaseNumber, request.CaseNumber},
            {NotifyParams.Time, request.ScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.DayMonthYear, request.ScheduledDateTime.ToEmailDateGbLocale()},
            {NotifyParams.DayMonthYearCy, request.ScheduledDateTime.ToEmailDateCyLocale()},
            {NotifyParams.Name, request.Name},
            {NotifyParams.StartTime, request.ScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.UserName, request.Username.ToLower()},
            {NotifyParams.TotalDays, request.TotalDays.ToString()},
        };
        return parameters;
    }

    public static Dictionary<string, string> MapToHearingAmendment(HearingAmendmentRequest request)
    {
        var parameters = new Dictionary<string, string>
        {
            {NotifyParams.CaseName, request.CaseName},
            {NotifyParams.UserName, request.Username.ToLower()},
            {NotifyParams.CaseNumber, request.CaseNumber},
            {NotifyParams.OldTime, request.PreviousScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.NewTime, request.NewScheduledDateTime.ToEmailTimeGbLocale()},
            {NotifyParams.OldDayMonthYear, request.PreviousScheduledDateTime.ToEmailDateGbLocale()},
            {NotifyParams.NewDayMonthYear, request.NewScheduledDateTime.ToEmailDateGbLocale()}
        };
        
        if (request.RoleName == RoleNames.Judge)
        {
            parameters.Add(NotifyParams.Judge, request.DisplayName);
            parameters.Add(NotifyParams.CourtroomAccountUserName, request.Username.ToLower());
        }

        if (request.RoleName == RoleNames.JudicialOfficeHolder)
        {
            parameters.Add(NotifyParams.JudicialOfficeHolder, request.Name);
        }

        if (request.RoleName == RoleNames.Individual)
        {
            parameters.Add(NotifyParams.Name, request.Name);
        }

        if (request.RoleName == RoleNames.Representative)
        {
            parameters.Add(NotifyParams.ClientName, request.Representee);
            parameters.Add(NotifyParams.SolicitorName, request.Name);
        }

        return parameters;
    }
}
