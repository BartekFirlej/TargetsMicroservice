using TargetsMicroservice.Models;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Responses;

namespace TargetsMicroservice.Services.Interfaces
{
    public interface ICrucialPlacesService
    {
        public Task<List<CrucialPlaceResponse>> GetCrucialPlaces();
        public Task<Crucialplace> AddCrucialPlace(CrucialPlaceRequest request);
    }
}
