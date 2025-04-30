using Application.Auth.Contracts;
using Application.Contracts;
using Application.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] string username)
    {
        var shelterManagerResult = await _shelterManagerService.CreateShelter(username);

        if (shelterManagerResult.IsFailure)
        {
            return BadRequest(shelterManagerResult.Error);
        }

        var token = _jwtService.GenerateToken(username,shelterManagerResult.Value); // Include shelterId in token

        return Ok(new { token });
    }
}

}
