using Application.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Extensions;
using Domain.Entities;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MonkeyShelterController : ControllerBase
    {
        private readonly IMonkeyService _monkeyService;
        private readonly ICheckupService _checkupService;
        public MonkeyShelterController(IMonkeyService monkeyService, ICheckupService checkupService)
        {
            _monkeyService = monkeyService;
            _checkupService = checkupService;
        }

        [HttpPost]
        public async Task<IActionResult> AdmitMonkeyToShelter(MonkeyEntryRequest request)
        {
            var result = await _monkeyService.AddMonkey(request);

            HttpContext.Items["Result"] = result;

            return result.ToActionResult();
        }

        [HttpDelete]
        public async Task<IActionResult> DepartMonkeyFromShelter(MonkeyDepartureRequest request)
        {
            var result = await _monkeyService.DepartMonkey(request);

            HttpContext.Items["Result"] = result;

            return result.ToActionResult();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateMonkeyWeight(MonkeyWeightRequest request)
        {
            var result = await _monkeyService.UpdateMonkeyWeight(request);

            HttpContext.Items["Result"] = result;

            return result.ToActionResult();
        }

        [HttpGet("/vetCheck")]
        public async Task<IActionResult> CheckForVetCheckup()
        {
            var result = await _checkupService.RetreiveMonkeysWithUpcomingVeteranaryCheckup();

            return Ok(new
            {
                ScheduledInTheNext30 = result.ScheduledInLessThan30Days,
                UpcomingVetChecks = result.ScheduledInMoreThan30Days
            });

        }

        [HttpGet("/species")]
        public async Task<ActionResult<List<MonkeyInfo>>> GetMonkeyBySpecies(MonkeySpecies species)
        {
             return Ok(await _monkeyService.GetMonkeyBySpecies(species));
        }

        [HttpGet("/monkeys")]
        public async Task<ActionResult<List<MonkeyInfo>>> GetMonkeysByArrivalDate(DateTime dateFrom, DateTime dateTo)
        {
            return Ok(await _monkeyService.GetMonkeysByDate(dateFrom, dateTo));
        }


        
    }
}
