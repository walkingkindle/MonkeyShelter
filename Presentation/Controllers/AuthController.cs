using Application.Contracts;
using Application.Contracts.Auth;
using Application.Contracts.Business;
using Application.Implementations;
using Application.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
/// <summary>
/// Handles authentication operations for shelter managers.
/// </summary>
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly IShelterService _shelterManagerService;

    public AuthController(IJwtService jwtService, IShelterService shelterManagerService)
    {
        _jwtService = jwtService;
        _shelterManagerService = shelterManagerService;
    }


    /// <summary>
    /// Logs in a shelter manager and generates a JWT token.
    /// </summary>
    /// <param name="username">The username of the shelter manager.</param>
    /// <returns>A JWT token if login is successful, or a BadRequest if the shelter creation fails.</returns>
    /// <response code="200">Successfully logged in and token generated.</response>
    /// <response code="400">Bad request, if shelter creation fails.</response>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var shelterManagerResult = await _shelterManagerService.CreateShelter(request.Username);

        if (shelterManagerResult.IsFailure)
        {
            return BadRequest(shelterManagerResult.Error);
        }

        var token = _jwtService.GenerateToken(request.Username,shelterManagerResult.Value); 
        return Ok(new { token });
    }
}

}
