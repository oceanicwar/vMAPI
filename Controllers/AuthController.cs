using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using vMAPI.Controllers.Models;
using vMAPI.Network;

namespace vMAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IConfiguration config;

    public AuthController(IConfiguration config)
    {
        this.config = config;
    }

    [AllowAnonymous]
    [HttpGet("status")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetAuthStatus()
    {
        var endpoint = config["Auth:Endpoint"];
        if (string.IsNullOrWhiteSpace(endpoint))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new BasicApiResult<object>(false, "An internal error occured."));
        }

        if (!int.TryParse(config["Auth:Port"], out int port))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new BasicApiResult<object>(false, "An internal error occured."));
        }

        bool isOnline = await NetworkUtilities.TestConnectionAsync(endpoint, port);

        return Ok(new BasicApiResult<object>(isOnline, $"Auth server is { (isOnline ? "online" : "offline") }."));
    }
}
