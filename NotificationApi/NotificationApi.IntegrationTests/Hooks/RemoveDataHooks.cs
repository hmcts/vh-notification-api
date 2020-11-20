using System.Linq;
using System.Threading.Tasks;
using NotificationApi.IntegrationTests.Contexts;
using TechTalk.SpecFlow;

namespace NotificationApi.IntegrationTests.Hooks
{
    [Binding]
    public static class RemoveDataHooks
    {
        [AfterScenario(Order = (int)HooksSequence.RemoveDataCreatedDuringTest)]
        public static async Task RemoveDataCreatedDuringTest(IntTestContext context)
        {
            await context.TestDataManager.RemoveNotifications(context.TestRun.NotificationsCreated.Select(x => x.Id));
        }

        [BeforeScenario(Order = (int)HooksSequence.RemoveNotifications)]
        [AfterScenario(Order = (int)HooksSequence.RemoveNotifications)]
        public static void RemoveNotificationTestData(IntTestContext context)
        {
            // Intentionally left empty
        }

        [AfterScenario(Order = (int)HooksSequence.RemoveServer)]
        public static void RemoveServer(IntTestContext context)
        {
            context.Server.Dispose();
        }
    }
}
