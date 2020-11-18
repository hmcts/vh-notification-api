namespace NotificationApi.IntegrationTests.Hooks
{
    internal enum HooksSequence
    {
        ConfigHooks = 1,
        RemoveDataCreatedDuringTest = 2,
        RemoveNotifications = 3,
        RemoveServer = 4
    }
}
