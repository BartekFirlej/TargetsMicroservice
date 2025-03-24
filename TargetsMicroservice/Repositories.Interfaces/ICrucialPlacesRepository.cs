using TargetsMicroservice.Models;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Responses;

namespace TargetsMicroservice.Repositories.Interfaces
{
    public interface ICrucialPlacesRepository
    {
        public Task<List<CrucialPlaceResponse>> GetCrucialPlaces();
        public Task<Crucialplace> AddCrucialPlace(CrucialPlaceRequest request);
    }
}
