using TargetsMicroservice.Models;

namespace TargetsMicroservice.Services.Interfaces
{
    public interface IFlightService
    {
        public Task<List<Flight>> GetFlights();
        public Task<Flight> GetFlightById(long flightID);
    }
}
