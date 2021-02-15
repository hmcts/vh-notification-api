using System.Threading.Tasks;
using NotificationApi.AcceptanceTests.Contexts;
using NotificationApi.Contract.Requests;
using TechTalk.SpecFlow;

namespace NotificationApi.AcceptanceTests.Steps
{
    [Binding]
    public class CallbackSteps
    {
        private readonly AcTestContext _context;
        private readonly CreateNotificationSteps _createNotificationSteps;
        private readonly CommonSteps _commonSteps;
        private NotificationCallbackRequest CallbackRequest { get; set; }

        public CallbackSteps(AcTestContext context)
        {
            _context = context;
            _createNotificationSteps = new CreateNotificationSteps(context);
            _commonSteps = new CommonSteps(context);
        }

        [Given(@"I have a notification that has been sent")]
        public async Task GivenIHaveANotificationThatHasBeenSent()
        {
            _createNotificationSteps.Given_I_Have_A_Request_To_Create_An_Email_Notification_For_New_Individual();
            await _commonSteps.WhenISendTheCreateNotificationRequest();
            await _commonSteps.ThenNotifyShouldHaveMyRequest();
        }

        [Given(@"I have a delivery receipt callback with a status delivered")]
        public void GivenIHaveADeliveryReceipt()
        {
            var notification = _context.RecentNotification;
            CallbackRequest = new NotificationCallbackRequest
            {
                Id = notification.id,
                Reference = notification.reference,
                Status = "delivered"
            };
        }
        
        [When(@"I send the callback request")]
        public async Task WhenISendTheCallbackRequest()
        {
            await _context.ExecuteApiRequest(() =>
                _context.ApiCallbackClient.HandleCallbackAsync(CallbackRequest));
        }
    }
}
