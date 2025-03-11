using TargetsMicroservice.Models;

namespace TargetsMicroservice.Repositories.Interfaces
{
    public interface IFlightRepository
    {
        public Task<List<Flight>> GetFlights();
        public Task<Flight> GetFlightById(long flightID);
    }
}
