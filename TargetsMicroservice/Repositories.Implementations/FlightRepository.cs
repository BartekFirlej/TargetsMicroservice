using Microsoft.EntityFrameworkCore;
using TargetsMicroservice.Models;
using TargetsMicroservice.Repositories.Interfaces;

namespace TargetsMicroservice.Repositories.Implementations
{
    public class FlightRepository : IFlightRepository
    {
        private readonly MagisterkaContext _dbContext;

        public FlightRepository(MagisterkaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Flight> GetFlightById(long flightID)
        {
            return await _dbContext.Flights.Where(f => f.Flightid == flightID).FirstOrDefaultAsync();
        }

        public async Task<List<Flight>> GetFlights()
        {
            return await _dbContext.Flights.ToListAsync();
        }
    }
}
