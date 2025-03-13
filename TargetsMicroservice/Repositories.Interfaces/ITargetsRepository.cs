using TargetsMicroservice.Models;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Responses;

namespace TargetsMicroservice.Repositories.Interfaces
{
    public interface ITargetsRepository
    {
        public Task<List<TargetResponse>> GetTargets();
        public Task<TargetResponse> GetTargetById(long targetId);
        public Task<Target> AddTarget(TargetDatabaseRequest target);
    }
}
