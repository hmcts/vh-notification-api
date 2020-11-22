using System;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Contract.Responses;
using NSwag.Annotations;

namespace NotificationApi.Controllers
{
    [Produces("application/json")]
    [Route("HealthAuth")]
    [ApiController]
    public class HealthCheckAuthController : ControllerBase
    {
        /// <summary>
        ///     Check Service Health
        /// </summary>
        /// <returns>Error if fails, otherwise OK status</returns>
        [HttpGet("health-auth")]
        [OpenApiOperation("CheckServiceHealthAuth")]
        [ProducesResponseType(typeof(HealthResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HealthResponse), (int) HttpStatusCode.InternalServerError)]
        [Authorize]
        public IActionResult HealthAuthAsync()
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
