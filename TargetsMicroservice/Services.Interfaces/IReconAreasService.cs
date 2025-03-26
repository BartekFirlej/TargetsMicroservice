using TargetsMicroservice.Models;
using TargetsMicroservice.Requests;

namespace TargetsMicroservice.Services.Interfaces
{
    public interface IReconAreasService
    {
        public Task<List<Reconarea>> GetReconAreas();
        public Task<Reconarea> AddReconArea(ReconAreaRequest request);
    }
}
