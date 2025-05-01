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
    /// <summary>
    /// Controller for managing monkey admissions, departures, and updates in the shelter.
    /// </summary>
    [ApiController]
    [Route("/api/monkeys")]
    public class MonkeyShelterController : ControllerBase
    {
        private readonly IMonkeyService _monkeyService;
        private readonly IShelterAuthorizationService _shelterAuthorizationService;
        public MonkeyShelterController(IMonkeyService monkeyService,
            IShelterAuthorizationService shelterAuthorizationService)
        {
            _monkeyService = monkeyService;
            _shelterAuthorizationService = shelterAuthorizationService;
        }

        /// <summary>
        /// Admits a new monkey to the shelter.
        /// </summary>
        /// <remarks>
        /// Requires an authenticated Shelter Manager with a valid JWT containing a <c>ShelterId</c> claim.
        /// The shelter must own the monkey to perform this action.
        /// </remarks>
        /// <param name="request">Details about the monkey to be admitted.</param>
        /// <returns>The admitted monkey's details if successful.</returns>
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

        /// <summary>
        /// Removes a monkey from the shelter by ID.
        /// </summary>
        /// <remarks>
        /// Requires an authenticated Shelter Manager with a valid JWT containing a <c>ShelterId</c> claim.
        /// The shelter must own the monkey to perform this action.
        /// </remarks>
        /// <param name="id">The ID of the monkey to remove.</param>
        /// <returns>204 No Content if successful; 403 or 400 otherwise.</returns>
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

        /// <summary>
        /// Updates a monkey's weight.
        /// </summary>
        /// <remarks>
        /// Requires an authenticated Shelter Manager with a valid JWT containing a <c>ShelterId</c> claim.
        /// The shelter must own the monkey to perform this action.
        /// </remarks>
        /// <param name="request">The monkey ID and the new weight value.</param>
        /// <returns>200 OK if updated; 403 if unauthorized.</returns>
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateMonkeyWeight([FromBody]MonkeyWeightRequest request)
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


        /// <summary>
        /// Retrieves all monkeys admitted in the past 3 months.
        /// </summary>
        /// <returns>A list of monkey report entries.</returns>
        [HttpGet("")]
        public async Task<ActionResult<List<MonkeyReportResponse>>> GetAllMonkeys()
        {
            var result = await _monkeyService.GetMonkeysByDate(new MonkeyDateRequest
            {
                DateFrom = DateTime.Today.AddMonths(-3),
                DateTo = DateTime.Today
            });

            if (result.IsFailure)
            {
                return BadRequest(result.Error);

            }

            return result.Value;

        }




        
    }
}
