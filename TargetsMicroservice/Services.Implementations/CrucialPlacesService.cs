using System.Diagnostics.CodeAnalysis;
using TargetsMicroservice.Models;
using TargetsMicroservice.Repositories.Interfaces;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Responses;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Services.Implementations
{
    public class CrucialPlacesService : ICrucialPlacesService
    {
        private readonly ICrucialPlacesRepository _crucialPlacesRepository;

        public CrucialPlacesService(ICrucialPlacesRepository crucialPlacesRepository)
        {
            _crucialPlacesRepository = crucialPlacesRepository;
        }

        public async Task<Crucialplace> AddCrucialPlace(CrucialPlaceRequest request)
        {
            return await _crucialPlacesRepository.AddCrucialPlace(request);
        }

        public async Task<List<CrucialPlaceResponse>> GetCrucialPlaces()
        {
            return await _crucialPlacesRepository.GetCrucialPlaces();
        }
    }
}
