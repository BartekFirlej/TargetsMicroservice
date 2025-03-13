using TargetsMicroservice.Models;

namespace TargetsMicroservice.Services.Interfaces
{
    public interface ITargetTypesService
    {
        public Task<Targettype> GetTargetTypeByName(string typeName);
    }
}
