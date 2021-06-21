using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleApi.Entities.Maps.Geocoding.Address.Request;
using WebAPI.Models;

namespace WebAPI
{
    public interface IDirectionService
    {
        public Task<Directions> GetDirectionsAsync(string origin, string destination, bool map);
    }
}
