using Application.Contracts;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IMonkeyService _monkeyService;

        public ReportsController(IMonkeyService monkeyService)
        {
            _monkeyService = monkeyService;
        }

        [HttpGet("/reports/monkeys-per-species")]
        public async Task<ActionResult<List<MonkeyInfo>>> GetMonkeyBySpecies(MonkeySpecies species)
        {
            return Ok(await _monkeyService.GetMonkeyBySpecies(species));
        }

        [HttpGet("/reports/arrivals-per-species")]
        public async Task<ActionResult<List<MonkeyInfo>>> GetMonkeysByArrivalDate(DateTime dateFrom, DateTime dateTo)
        {
            return Ok(await _monkeyService.GetMonkeysByDate(dateFrom, dateTo));
        }
    }
}
