using TargetsMicroservice.Models;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Services.Implementations
{
    public class FlightsService : IFlightsService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightsService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<Flight> CreateFlight(FlightBeginRequest request)
        {
            return await _flightRepository.CreateFlight(request);
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
