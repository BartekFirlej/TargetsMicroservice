using TargetsMicroservice.Models;
using TargetsMicroservice.Requests;

namespace TargetsMicroservice.Services.Interfaces
{
    public interface IFlightsService
    {
        public Task<List<Flight>> GetFlights();
        public Task<Flight> GetFlightById(long flightID);
        public Task<Flight> CreateFlight(FlightBeginRequest request);
        public Task<Flight> EndFlight(FlightEndRequest request);
    }
}
