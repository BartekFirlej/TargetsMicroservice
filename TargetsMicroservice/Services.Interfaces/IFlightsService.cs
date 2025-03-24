using TargetsMicroservice.Models;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Responses;

namespace TargetsMicroservice.Services.Interfaces
{
    public interface IFlightsService
    {
        public Task<List<FlightResponse>> GetFlights();
        public Task<Flight> GetFlightById(long flightID);
        public Task<FlightResponse> GetFlightResponseById(long flightID);
        public Task<Flight> CreateFlight(FlightBeginRequest request);
        public Task<Flight> EndFlight(FlightEndRequest request);
    }
}
