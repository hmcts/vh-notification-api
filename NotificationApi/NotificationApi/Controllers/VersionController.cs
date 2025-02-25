using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace NotificationApi.Controllers;

[ExcludeFromCodeCoverage]
[Route("api/[controller]")]
[AllowAnonymous]
public class VersionController : ControllerBase
{
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public IActionResult GetVersion()
    {
        var version = AppVersionRetriever.GetAppVersion();
        return Ok(version);
    }
}
