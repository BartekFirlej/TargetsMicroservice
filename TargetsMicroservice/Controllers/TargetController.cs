using Microsoft.AspNetCore.Mvc;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Controllers
{
    [ApiController]
    [Route("targets")]
    public class TargetController : ControllerBase
    {
        private readonly ITargetsService _targetService;

        public TargetController(ITargetsService targetService)
        {
            _targetService = targetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var targets = await _targetService.GetTargets();
            return Ok(targets);
        }

        [HttpPost]
        public async Task<IActionResult> PostTarget(TargetRequest request)
        {
            var target = await _targetService.AddTarget(request);
            return CreatedAtAction(nameof(PostTarget),target);
        }
    }
}
