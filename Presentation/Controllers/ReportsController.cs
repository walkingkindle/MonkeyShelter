using Application.Contracts;
using Application.Contracts.Business;
using Application.Implementations;
using Application.Shared.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;

namespace Presentation.Controllers
{
    /// <summary>
    /// Provides endpoints for generating monkey-related reports.
    /// </summary>
    [ApiController]
    [Route("/api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IMonkeyService _monkeyService;

        /// <summary>
        /// Provides endpoints for generating monkey-related reports.
        /// </summary>
        public ReportsController(IMonkeyService monkeyService)
        {
            _monkeyService = monkeyService;
        }

        /// <summary>
        /// Gets the number of monkeys per species.
        /// </summary>
        /// <param name="species">The monkey species to filter by.</param>
        /// <returns>A list of monkey report entries grouped by species.</returns>
        [HttpGet("monkeys-per-species")]
        public async Task<ActionResult<List<MonkeyReportResponse>>> GetMonkeyBySpecies(MonkeySpecies species) { 

        var result = await _monkeyService.GetMonkeyBySpecies(species);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }


        /// <summary>
        /// Gets monkey arrival counts between two dates.
        /// </summary>
        /// <param name="dateFrom">Start date.</param>
        /// <param name="dateTo">End date.</param>
        /// <returns>A list of monkey report entries grouped by arrival date.</returns>

        [HttpGet("arrivals-per-date")]
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
