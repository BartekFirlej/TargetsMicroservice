using TargetsMicroservice.Models;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Responses;

namespace TargetsMicroservice.Repositories.Interfaces
{
    public interface IFlightsRepository
    {
        public Task<List<FlightResponse>> GetFlights();
        public Task<FlightResponse> GetFlightResponseById(long flightID);
        public Task<Flight> GetFlightById(long flightID);
        public Task<Flight> CreateFlight(FlightBeginRequest request);
        public Task<Flight> EndFlight(Flight flight, DateTime flightEndTime);
    }
}
