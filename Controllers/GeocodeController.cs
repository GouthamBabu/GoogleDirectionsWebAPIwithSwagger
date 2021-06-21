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
    public class GeocodeController : ControllerBase
    {
        private readonly IGeocodeService _GeocodeService;
        public GeocodeController(IGeocodeService geocodeService)
        {
            _GeocodeService = geocodeService;
        }

        [Route("/api/geocode")]
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return BadRequest();
            }
            try
            {
                return Ok(await _GeocodeService.GeocodeAddressAsync(address));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
