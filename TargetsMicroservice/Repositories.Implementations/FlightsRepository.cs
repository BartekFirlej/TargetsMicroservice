using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using TargetsMicroservice.Models;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Responses;

namespace TargetsMicroservice.Repositories.Implementations
{
    public class FlightsRepository : IFlightsRepository
    {
        private readonly MagisterkaContext _dbContext;

        public FlightsRepository(MagisterkaContext dbContext)
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
                Comment = request.Comment,
                VideoStream = request.VideoStream
            };
            var addedFlight = await _dbContext.Flights.AddAsync(flight);
            await _dbContext.SaveChangesAsync();
            return addedFlight.Entity;
        }

        public async Task<FlightResponse> GetFlightResponseById(long flightID)
        {
            return await _dbContext.Flights.Where(f => f.Flightid == flightID).Select(f => new FlightResponse
            {
                Flightid = f.Flightid,
                Begintime = f.Begintime,
                Endtime = f.Endtime,
                Comment = f.Comment,
                Operatorid = f.Operatorid,
                VideoStream = f.VideoStream,
                X = (float)f.Beginpoint.X,
                Y = (float)f.Beginpoint.Y,
                Z = (float)f.Beginpoint.Z
            }).FirstOrDefaultAsync();
        }

        public async Task<List<FlightResponse>> GetFlights()
        {
            return await _dbContext.Flights.Select(f => new FlightResponse
            {
                Flightid = f.Flightid,
                Begintime = f.Begintime,
                Endtime = f.Endtime,
                Comment = f.Comment,
                Operatorid = f.Operatorid,
                VideoStream = f.VideoStream,
                X = (float)f.Beginpoint.X,
                Y = (float)f.Beginpoint.Y,
                Z = (float)f.Beginpoint.Z
            }).ToListAsync();
        }

        public async Task<Flight> EndFlight(Flight flight, DateTime flightEndTime)
        {
            flight.Endtime = flightEndTime;
            await _dbContext.SaveChangesAsync();
            return flight;
        }

        public async Task<Flight> GetFlightById(long flightID)
        {
            return await _dbContext.Flights.Where(f => f.Flightid == flightID).FirstOrDefaultAsync();
        }
    }
}
