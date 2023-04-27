using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Contract.Responses;
using NotificationApi.DAL.Models;
using NotificationApi.DAL.Queries;
using NotificationApi.DAL.Queries.Core;
using NSwag.Annotations;


namespace NotificationApi.Controllers
{
    [Produces("application/json")]
    [AllowAnonymous]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly IQueryHandler _queryHandler;
        public HealthCheckController(IQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
        }
        /// <summary>
        ///     Check Service Health
        /// </summary>
        /// <returns>Error if fails, otherwise OK status</returns>
        [HttpGet("HealthCheck/health")]
        [HttpGet("health/liveness")]
        [OpenApiOperation("CheckServiceHealthAuth")]
        [ProducesResponseType(typeof(HealthResponse), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HealthResponse), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> HealthAsync()
        {
            var response = new HealthResponse {AppVersion = GetApplicationVersion()};
            try
            {
                var result = await _queryHandler.Handle<DbHealthCheckQuery, DbHealthCheckResult>(new DbHealthCheckQuery());
                response.DatabaseHealth.Successful = result.CanConnect;
            }
            catch (Exception ex)
            {
                response.DatabaseHealth.Successful = false;
                response.DatabaseHealth.ErrorMessage = ex.Message;
                response.DatabaseHealth.Data = ex.Data;
            }
            
            return !response.DatabaseHealth.Successful ? StatusCode((int)HttpStatusCode.InternalServerError, response) : Ok(response);
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
