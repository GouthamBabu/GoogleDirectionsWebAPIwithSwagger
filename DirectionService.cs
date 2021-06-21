using GoogleApi.Entities.Common;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using GoogleApi;
using System;
using GoogleApi.Entities.Maps.StaticMaps.Request;

namespace WebAPI
{
    public class DirectionService : IDirectionService
    {
        private readonly IConfiguration _configuration;

        public DirectionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Directions> GetDirectionsAsync(string origin, string destination, bool map)
        {
            var request = new DirectionsRequest()
            {
                Key = _configuration["apikey"],
                Origin = new Location(origin),
                Destination = new Location(destination)
                //request.DepartureTime = UserParameters.DepartureTime;
            };
            DirectionsResponse googleResponse;
            try
            {
                googleResponse = await GoogleMaps.Directions.QueryAsync(request);
            }
            catch (Exception e)
            {
                throw;
            }

            var DirectionsResponse = new Directions();
            DirectionsResponse.Start = origin;
            DirectionsResponse.End = destination;

            if (!googleResponse.Routes.Any())
            {
                return null;
            }
            foreach (var Leg in googleResponse.Routes.First().Legs)
            {
                DirectionsResponse.TotalDistance += Leg.Distance.Value;
                DirectionsResponse.TotalDuration += Leg.Duration.Value;
                DirectionsResponse.Route = new List<string>();
                
                foreach (var step in Leg.Steps)
                {
                    DirectionsResponse.Route.Add($"\n{step.HtmlInstructions.ToString()}");
                }

                if (map)
                {
                    var mapPoints = new List<Location>();
                    var mapPaths = new List<MapPath>();
                    foreach (var step in Leg.Steps)
                    {
                        mapPoints.Add(step.StartLocation);
                    }
                    mapPaths.Add(new MapPath() { Points = mapPoints });
                    var staticMapRequest = new StaticMapsRequest()
                    {
                        Paths = mapPaths,
                        Key = request.Key
                    };
                    DirectionsResponse.MapURL = staticMapRequest.GetUri().ToString();
                }
            }
            return DirectionsResponse;
        }
    }
}