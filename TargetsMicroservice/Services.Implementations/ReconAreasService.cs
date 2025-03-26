using TargetsMicroservice.Models;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Services.Implementations
{
    public class ReconAreasService : IReconAreasService
    {
        private readonly IReconAreasRepository _reconAreasRepository;

        public ReconAreasService(IReconAreasRepository reconAreasRepository)
        {
            _reconAreasRepository = reconAreasRepository;
        }

        public async Task<Reconarea> AddReconArea(ReconAreaRequest request)
        {
            return await _reconAreasRepository.AddReconArea(request);
        }

        public async Task<List<Reconarea>> GetReconAreas()
        {
            return await _reconAreasRepository.GetReconAreas();
        }
    }
}
