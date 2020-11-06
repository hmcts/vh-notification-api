using System;
using System.Linq;
using System.Threading.Tasks;
using NotifyApi.IntegrationTests.Contexts;
using TechTalk.SpecFlow;

namespace NotifyApi.IntegrationTests.Hooks
{
    [Binding]
    public static class RemoveDataHooks
    {
        [AfterScenario(Order = (int)HooksSequence.RemoveDataCreatedDuringTest)]
        public static async Task RemoveDataCreatedDuringTest(TestContext context)
        {
            await context.TestDataManager.RemoveNotifications(context.TestRun.NotificationsCreated.Select(x => x.Id));
        }

        [BeforeScenario(Order = (int)HooksSequence.RemoveNotifications)]
        [AfterScenario(Order = (int)HooksSequence.RemoveNotifications)]
        public static Task RemoveNotificationTestData(TestContext context)
        {
            throw new NotImplementedException();
        }

        [AfterScenario(Order = (int)HooksSequence.RemoveServer)]
        public static void RemoveServer(TestContext context)
        {
            context.Server.Dispose();
        }
    }
}
