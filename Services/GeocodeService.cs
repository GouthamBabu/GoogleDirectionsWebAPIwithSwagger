using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using System.Threading.Tasks;
using System.Configuration;
using WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using GoogleApi;
using Microsoft.Extensions.Configuration;
using GoogleApi.Entities.Maps.Geocoding.Address.Request;
using System;
using GoogleApi.Entities.Maps.StaticMaps.Request;
using Geometry = WebAPI.Models.Geometry;

namespace WebAPI
{
    public class GeocodeService : IGeocodeService
    {
        private readonly IConfiguration _configuration;

        public GeocodeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Geocode> GeocodeAddressAsync(string address)
        {
            try
            {
                var request = new AddressGeocodeRequest
                {
                    Address = address,
                    Key = _configuration["apikey"]
                };
                var response = await GoogleMaps.AddressGeocode.QueryAsync(request);
                var results = response.Results.FirstOrDefault();
                var geocode = new Geocode
                {
                    FormattedAddress = results.FormattedAddress,
                    PlaceId = results.PlaceId,
                    PartialMatch = results.PartialMatch,
                    Geometry = new Geometry
                    {
                        Latitude = results.Geometry.Location.Latitude,
                        Longitude = results.Geometry.Location.Longitude
                    }
                };
                return geocode;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}
