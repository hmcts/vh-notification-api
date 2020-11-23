using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;

namespace NotificationApi.Common
{
    /// <summary>
    /// Adds bad request response bodies to AppInsights for better troubleshooting
    /// </summary>
    public class AppInsightsTelemetry : ITelemetryInitializer
    {
        readonly IHttpContextAccessor _httpContextAccessor;

        public AppInsightsTelemetry(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleName = "vh-notification-api";
            
            if (!(telemetry is RequestTelemetry requestTelemetry))
            {
                return;
            }

            if (!IsReadableBadRequest(requestTelemetry))
            {
                return;
            }

            // Check response body
            var responseBody = (string) _httpContextAccessor.HttpContext.Items["responseBody"];
            if (responseBody != null)
            {
                requestTelemetry.Properties.Add("responseBody", responseBody);
            }
        }

        private bool IsReadableBadRequest(RequestTelemetry telemetry)
        {
            return _httpContextAccessor.HttpContext.Request.Body.CanRead
                && telemetry.ResponseCode == "400";
        }
    }
}
