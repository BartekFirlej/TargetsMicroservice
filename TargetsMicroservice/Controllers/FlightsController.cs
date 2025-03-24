using Microsoft.AspNetCore.Mvc;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Controllers
{
    [ApiController]
    [Route("flights")]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightsService _flightService;

        public FlightsController(IFlightsService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var flights = await _flightService.GetFlights();
            return Ok(flights);
        }

        [HttpGet("flightID")]
        public async Task<IActionResult> GetFlightByID(long flightID)
        {
            var flight = await _flightService.GetFlightResponseById(flightID);
            return Ok(flight);
        }
    }
}
