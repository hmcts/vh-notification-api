using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Contract.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace NotificationApi.Controller
{
    [Produces("application/json")]
    [Route("Health")]
    [AllowAnonymous]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        /// <summary>
        ///     Check Service Health
        /// </summary>
        /// <returns>Error if fails, otherwise OK status</returns>
        [HttpGet("health")]
        [SwaggerOperation(OperationId = "CheckServiceHealth")]
        [ProducesResponseType(typeof(HealthResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HealthResponse), (int) HttpStatusCode.InternalServerError)]
        public IActionResult HealthAsync()
        {
            var response = new HealthResponse {Version = GetApplicationVersion()};
            return Ok(response);
        }

        private static AppVersionResponse GetApplicationVersion()
        {
            var applicationVersion = new AppVersionResponse()
            {
                FileVersion = GetExecutingAssemblyAttribute<AssemblyFileVersionAttribute>(a => a.Version),
                InformationVersion =
                    GetExecutingAssemblyAttribute<AssemblyInformationalVersionAttribute>(a => a.InformationalVersion)
            };
            return applicationVersion;
        }

        private static string GetExecutingAssemblyAttribute<T>(Func<T, string> value) where T : Attribute
        {
            var attribute = (T) Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(T));
            return value.Invoke(attribute);
        }
    }
}
