using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using TargetsMicroservice.Models;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Responses;

namespace TargetsMicroservice.Repositories.Implementations
{
    public class TargetsRepository : ITargetsRepository
    {
        private readonly MagisterkaContext _dbContext;

        public TargetsRepository(MagisterkaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Target> AddTarget(TargetDatabaseRequest target)
        {
            var targetToAdd = new Target
            {
                Comment = target.Comment,
                Detectiontime = target.Detectiontime,
                Flightid = target.Flightid,
                Imagelink = target.Image,
                Location = new Point(new CoordinateZ(target.X, target.Y, target.Z))
                {
                    SRID = 4326
                },
                Targettypeid = target.Targettypeid,
                Targetid = target.Targetid
            };
            var addedTarget = await _dbContext.Targets.AddAsync(targetToAdd);
            await _dbContext.SaveChangesAsync();
            return addedTarget.Entity;
        }

        public async Task<TargetResponse> GetTargetById(long targetId)
        {
            return await _dbContext.Targets.Include(t => t.Targettype).Select(t => new TargetResponse
            {
                Targetid = t.Targetid,
                Targettypeid = t.Targettypeid,
                Flightid = t.Flightid,
                Imagelink = t.Imagelink,
                X = (float)t.Location.X,
                Y = (float)t.Location.Y,
                Z = (float)t.Location.Z,
                Comment = t.Comment,
                Detectiontime = t.Detectiontime,
                Targettypename = t.Targettype.Name
            }).Where(t => t.Targetid == targetId).FirstOrDefaultAsync();
        }

        public async Task<List<TargetResponse>> GetTargets()
        {
            return await _dbContext.Targets.Include(t => t.Targettype).Select(t => new TargetResponse
            {
                Targetid = t.Targetid,
                Targettypeid = t.Targettypeid,
                Flightid = t.Flightid,
                Imagelink = t.Imagelink,
                X = (float)t.Location.X,
                Y = (float)t.Location.Y,
                Z = (float)t.Location.Z,
                Comment = t.Comment,
                Detectiontime = t.Detectiontime,
                Targettypename = t.Targettype.Name
            }).ToListAsync();
        }
    }
}
