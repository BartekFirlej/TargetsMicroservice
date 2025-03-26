using TargetsMicroservice.Models;
using TargetsMicroservice.Requests;

namespace TargetsMicroservice.Repositories.Interfaces
{
    public interface IReconAreasRepository
    {
        public Task<List<Reconarea>> GetReconAreas();
        public Task<Reconarea> AddReconArea(ReconAreaRequest request);
    }
}
