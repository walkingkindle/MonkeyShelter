using Application.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Application.Extensions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MonkeyShelterController : ControllerBase
    {
        private readonly IMonkeyService _monkeyService;
        public MonkeyShelterController(IMonkeyService monkeyService)
        {
            _monkeyService = monkeyService;
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

        
    }
}
