using Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Application.Extensions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Application.Shared.Models;
using Application.Contracts.Business;
using Application.Contracts.Auth;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/api/monkeys")]
    public class MonkeyShelterController : ControllerBase
    {
        private readonly IMonkeyService _monkeyService;
        private readonly ICheckupService _checkupService;
        private readonly IShelterAuthorizationService _shelterAuthorizationService;
        public MonkeyShelterController(IMonkeyService monkeyService, ICheckupService checkupService,
            IShelterAuthorizationService shelterAuthorizationService)
        {
            _monkeyService = monkeyService;
            _checkupService = checkupService;
            _shelterAuthorizationService = shelterAuthorizationService;
        }

        [Authorize]
        [HttpPost("")]
        public async Task<ActionResult<Monkey>> AdmitMonkeyToShelter(MonkeyEntryRequest request)
        {
            var shelterIdClaim = User.FindFirst("ShelterId");

            if (shelterIdClaim == null || !int.TryParse(shelterIdClaim.Value, out var shelterId))
            {
                return Unauthorized("ShelterId is missing or invalid in the token.");
            }

            var result = await _monkeyService.AddMonkey(request, shelterId);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DepartMonkeyFromShelter(int id)
        {
            var shelterIdClaim = User.FindFirst("ShelterId");

            if (shelterIdClaim == null || !int.TryParse(shelterIdClaim.Value, out var shelterId))
            {
                return Unauthorized("ShelterId is missing or invalid in the token.");
            }

            if (!await _shelterAuthorizationService.IsMonkeyOwnedByShelterAsync(id, shelterId))
                return Forbid();

            var request = new MonkeyDepartureRequest { MonkeyId = id };

            var result = await _monkeyService.DepartMonkey(request);

            HttpContext.Items["Result"] = result;

            return result.ToActionResult();
        }

        [Authorize]
        [HttpPatch("weight/{id}")]
        public async Task<IActionResult> UpdateMonkeyWeight(MonkeyWeightRequest request)
        {
            var shelterIdClaim = User.FindFirst("ShelterId");

            if (shelterIdClaim == null || !int.TryParse(shelterIdClaim.Value, out var shelterId))
            {
                return Unauthorized("ShelterId is missing or invalid in the token.");
            }

            if (!await _shelterAuthorizationService.IsMonkeyOwnedByShelterAsync(request.MonkeyId, shelterId))
                return Forbid();

            var result = await _monkeyService.UpdateMonkeyWeight(request);

            HttpContext.Items["Result"] = result;

            return result.ToActionResult();
        }

        [HttpGet("vet-checks")]
        public async Task<IActionResult> CheckForVetCheckup()
        {
            var result = await _checkupService.RetreiveMonkeysWithUpcomingVeteranaryCheckup();

            return Ok(new
            {
                upcomingCheckups = result.ScheduledCheckups,
            });

        }




        
    }
}
