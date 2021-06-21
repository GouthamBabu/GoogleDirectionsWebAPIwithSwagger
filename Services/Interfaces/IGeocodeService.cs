using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleApi.Entities.Maps.Geocoding.Address.Request;
using WebAPI.Models;

namespace WebAPI
{
    public interface IGeocodeService
    {
        public Task<Geocode> GeocodeAddressAsync(string address);
    }
}
