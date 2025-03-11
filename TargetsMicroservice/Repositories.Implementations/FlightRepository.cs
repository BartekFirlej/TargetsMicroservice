using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using TargetsMicroservice.Models;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Requests;

namespace TargetsMicroservice.Repositories.Implementations
{
    public class FlightRepository : IFlightRepository
    {
        private readonly MagisterkaContext _dbContext;

        public FlightRepository(MagisterkaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Flight> CreateFlight(FlightBeginRequest request)
        {
            var flight = new Flight()
            {
                Begintime = request.BeginTime,
                Beginpoint = new Point(new CoordinateZ(request.X, request.Y, request.Z))
                {
                    SRID = 4326
                },
                Operatorid = request.OperatorID,
                Flightid = request.FlightID,
                Comment = request.Comment
            };
            var addedFlight = await _dbContext.Flights.AddAsync(flight);
            await _dbContext.SaveChangesAsync();
            return addedFlight.Entity;
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
