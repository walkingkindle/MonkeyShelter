using Application.Contracts;
using Application.Contracts.Business;
using Application.Implementations;
using Application.Shared.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IMonkeyService _monkeyService;

        public ReportsController(IMonkeyService monkeyService)
        {
            _monkeyService = monkeyService;
        }

        [HttpGet("monkeys-per-species")]
        public async Task<ActionResult<List<MonkeyReportResponse>>> GetMonkeyBySpecies(MonkeySpecies species)
        {
            var result = await _monkeyService.GetMonkeyBySpecies(species);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }

        [HttpGet("arrivals-per-species")]
        public async Task<ActionResult<List<MonkeyReportResponse>>> GetMonkeysByArrivalDate(DateTime dateFrom, DateTime dateTo)
        {
            var result = await _monkeyService.GetMonkeysByDate(new MonkeyDateRequest { DateFrom = dateFrom, DateTo = dateTo });

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }
    }
}
