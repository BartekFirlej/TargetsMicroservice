using Microsoft.AspNetCore.Mvc;
using TargetsMicroservice.Requests;
using TargetsMicroservice.Services.Interfaces;

namespace TargetsMicroservice.Controllers
{
    [ApiController]
    [Route("crucialplaces")]
    public class CrucialPlacesController : ControllerBase
    {
        private readonly ICrucialPlacesService _crucialPlacesService;

        public CrucialPlacesController(ICrucialPlacesService crucialPlacesService)
        {
            _crucialPlacesService = crucialPlacesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var crucialPlaces = await _crucialPlacesService.GetCrucialPlaces();
            return Ok(crucialPlaces);
        }

        [HttpPost]
        public async Task<IActionResult> PostCrucialPlace(CrucialPlaceRequest request)
        {
            var crucialPlace = await _crucialPlacesService.AddCrucialPlace(request);
            return CreatedAtAction(nameof(PostCrucialPlace), crucialPlace.Crucialplaceid);
        }
    }
}
