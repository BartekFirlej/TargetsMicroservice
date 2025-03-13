using TargetsMicroservice.Requests;
using TargetsMicroservice.Responses;

namespace TargetsMicroservice.Services.Interfaces
{
    public interface ITargetsService
    {
        public Task<List<TargetResponse>> GetTargets();
        public Task<TargetResponse> GetTargetById(long targetId);
        public Task<TargetResponse> AddTarget(TargetRequest target);
    }
}
