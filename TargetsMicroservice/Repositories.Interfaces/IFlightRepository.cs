using TargetsMicroservice.Models;
using TargetsMicroservice.Requests;

namespace TargetsMicroservice.Repositories.Interfaces
{
    public interface IFlightRepository
    {
        public Task<List<Flight>> GetFlights();
        public Task<Flight> GetFlightById(long flightID);
        public Task<Flight> CreateFlight(FlightBeginRequest request);
        public Task<Flight> EndFlight(Flight flight, DateTime flightEndTime);
    }
}
