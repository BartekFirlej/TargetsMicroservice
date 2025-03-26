using Microsoft.EntityFrameworkCore;
using TargetsMicroservice.Models;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Requests;

namespace TargetsMicroservice.Repositories.Implementations
{
    public class ReconAreasRepository : IReconAreasRepository
    {
        private readonly MagisterkaContext _dbContext;

        public ReconAreasRepository(MagisterkaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Reconarea> AddReconArea(ReconAreaRequest request)
        {
            var reconArea = new Reconarea
            {
                Area = request.Area,
                Areashape = request.Areashape,
                Comment = request.Comment,
                Operatorid = request.Operatorid,
                Periodfrom = request.Periodfrom,
                Periodto = request.Periodto,
                Platoonid = request.Platoonid,
                Reconareaid = request.Reconareaid,
                Teamid = request.Teamid
            };
            var addedReconArea = await _dbContext.Reconareas.AddAsync(reconArea);
            await _dbContext.SaveChangesAsync();
            return addedReconArea.Entity;

        }

        public async Task<List<Reconarea>> GetReconAreas()
        {
            return await _dbContext.Reconareas.ToListAsync();
        }
    }
}
