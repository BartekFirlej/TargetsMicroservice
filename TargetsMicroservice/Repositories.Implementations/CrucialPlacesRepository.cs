using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using TargetsMicroservice.Models;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Responses;

namespace TargetsMicroservice.Repositories.Implementations
{
    public class CrucialPlacesRepository : ICrucialPlacesRepository
    {
        private readonly MagisterkaContext _dbContext;

        public CrucialPlacesRepository(MagisterkaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Crucialplace> AddCrucialPlace(CrucialPlaceRequest request)
        {
            var crucialPlace = new Crucialplace
            {
                Comment = request.Comment,
                Crucialplaceid = request.ObjectId,
                Location = new Point(new CoordinateZ(request.X, request.Y))
                {
                    SRID = 4326
                }
            };
            var addedCrucialPlace = await _dbContext.Crucialplaces.AddAsync(crucialPlace);
            await _dbContext.SaveChangesAsync();
            return addedCrucialPlace.Entity;
        }

        public async Task<List<CrucialPlaceResponse>> GetCrucialPlaces()
        {
            return await _dbContext.Crucialplaces.Select(c => new CrucialPlaceResponse
            {
                Crucialplaceid = c.Crucialplaceid,
                X = (float)c.Location.X,
                Y = (float)c.Location.Y,
                Comment = c.Comment
            }).ToListAsync();
        }
    }
}
