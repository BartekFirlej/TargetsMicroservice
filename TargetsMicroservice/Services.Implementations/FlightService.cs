using TargetsMicroservice.Models;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Services.Implementations
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<Flight> GetFlightById(long flightID)
        {
            return await _flightRepository.GetFlightById(flightID);
        }

        public async Task<List<Flight>> GetFlights()
        {
            return await _flightRepository.GetFlights();
        }
    }
}
