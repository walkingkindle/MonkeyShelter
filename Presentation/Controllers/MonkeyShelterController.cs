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

        [HttpPost("/monkeys")]
        public async Task<IActionResult> AdmitMonkeyToShelter(MonkeyEntryRequest request)
        {
            var result = await _monkeyService.AddMonkey(request);

            HttpContext.Items["Result"] = result;

            return result.ToActionResult();
        }

        [HttpDelete("/monkeys/{id}")]
        public async Task<IActionResult> DepartMonkeyFromShelter(MonkeyDepartureRequest request)
        {
            var result = await _monkeyService.DepartMonkey(request);

            HttpContext.Items["Result"] = result;

            return result.ToActionResult();
        }

        [HttpPatch("/monkeys/{id}/weight")]
        public async Task<IActionResult> UpdateMonkeyWeight(MonkeyWeightRequest request)
        {
            var result = await _monkeyService.UpdateMonkeyWeight(request);

            HttpContext.Items["Result"] = result;

            return result.ToActionResult();
        }

        [HttpGet("/monkeys/{id}/vet-check")]
        public async Task<IActionResult> CheckForVetCheckup()
        {
            var result = await _checkupService.RetreiveMonkeysWithUpcomingVeteranaryCheckup();

            return Ok(new
            {
                ScheduledInTheNext30 = result.ScheduledInLessThan30Days,
                UpcomingVetChecks = result.ScheduledInMoreThan30Days
            });

        }




        
    }
}
