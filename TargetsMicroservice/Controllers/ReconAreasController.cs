using Microsoft.AspNetCore.Mvc;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Services.Implementations;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Controllers
{
    [ApiController]
    [Route("reconareas")]
    public class ReconAreasController : ControllerBase
    {
        private readonly IReconAreasService _reconAreasService;

        public ReconAreasController(IReconAreasService reconAreasService)
        {
            _reconAreasService = reconAreasService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reconareas = await _reconAreasService.GetReconAreas();
            return Ok(reconareas);
        }

        [HttpPost]
        public async Task<IActionResult> PostReconArea(ReconAreaRequest request)
        {
            var reconarea = await _reconAreasService.AddReconArea(request);
            return Ok(reconarea);
        }
    }
}
