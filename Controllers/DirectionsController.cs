using GoogleApi.Entities.Maps.Geocoding.Address.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI;
using Microsoft.AspNetCore.Http;

namespace WebAPI
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DirectionsController : ControllerBase
    {
        private readonly IDirectionService _directionService;
        public DirectionsController(IDirectionService directionService)
        {
            _directionService = directionService;
        }

        [Route("/api/directions")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string origin, string destination ,bool map)
        {
            if (string.IsNullOrEmpty(origin)|string.IsNullOrEmpty(destination))
            {
                return BadRequest();
            }
            try
            {
                var Response = await _directionService.GetDirectionsAsync(origin, destination, map);
                if (Response is null)
                {
                    return StatusCode(404);
                }
                return Ok(Response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
