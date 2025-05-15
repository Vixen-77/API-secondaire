/*using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace YourApp.Services
{
    public interface IAmbulanceSimulationService
    {
        Task StartSimulationAsync(string simulationId);
        Task ChangeDestinationAsync(string simulationId);
    }

    public class AmbulanceSimulationService : IAmbulanceSimulationService
    {
        private readonly HttpClient _httpClient;
        private readonly IHubContext<AmbulanceHub> _hubContext;
        private readonly Random _rnd = new();
        private readonly (double lat, double lon) _centre = (36.7400, 2.9510);

        public AmbulanceSimulationService(HttpClient httpClient,
                                          IHubContext<AmbulanceHub> hubContext)
        {
            _httpClient = httpClient;
            _hubContext = hubContext;
        }

        public async Task StartSimulationAsync(string simulationId)
        {
            var basePos = _centre;
            var dest = GenerateRandomPoint(_centre.lat, _centre.lon, 3000);
            await RunRouteAsync(simulationId, basePos, dest);
            // Retour à la base
            await RunRouteAsync(simulationId, dest, basePos);
        }

        public async Task ChangeDestinationAsync(string simulationId)
        {
            // Génère une nouvelle destination aléatoire
            var current = _centre; // ou stocker dernière position si besoin
            var newDest = GenerateRandomPoint(_centre.lat, _centre.lon, 3000);
            await RunRouteAsync(simulationId, current, newDest);
        }

        private async Task RunRouteAsync(string simulationId, (double lat, double lon) start, (double lat, double lon) end)
        {
            var route = await GetRouteAsync(start, end);
            var coords = route.routes[0].geometry.coordinates;
            foreach (var coord in coords)
            {
                await _hubContext.Clients.Group(simulationId)
                    .SendAsync("ReceivePosition", new { lat = coord[1], lon = coord[0] });
                await Task.Delay(500);
            }
        }

        private (double lat, double lon) GenerateRandomPoint(double lat0, double lon0, double radiusMeters)
        {
            double radiusKm = radiusMeters / 1000.0;
            double lat0Rad = ToRadians(lat0);
            double lon0Rad = ToRadians(lon0);
            double u = _rnd.NextDouble();
            double v = _rnd.NextDouble();
            double w = radiusKm / 6371.0;
            double randDist = Math.Acos(1 - u * (1 - Math.Cos(w)));
            double randBrg = 2 * Math.PI * v;

            double latRad = Math.Asin(Math.Sin(lat0Rad) * Math.Cos(randDist)
                           + Math.Cos(lat0Rad) * Math.Sin(randDist) * Math.Cos(randBrg));
            double lonRad = lon0Rad + Math.Atan2(
                           Math.Sin(randBrg) * Math.Sin(randDist) * Math.Cos(lat0Rad),
                           Math.Cos(randDist) - Math.Sin(lat0Rad) * Math.Sin(latRad));

            return (ToDegrees(latRad), ToDegrees(lonRad));
        }

        private async Task<GeoJsonRoute> GetRouteAsync((double lat, double lon) start,
                                                      (double lat, double lon) end)
        {
            string url = $"http://localhost:5000/route/v1/driving/" +
                         $"{start.lon},{start.lat};{end.lon},{end.lat}?overview=full&geometries=geojson";
            var json = await _httpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<GeoJsonRoute>(json)!;
        }

        private static double ToRadians(double deg) => deg * Math.PI / 180;
        private static double ToDegrees(double rad) => rad * 180 / Math.PI;
    }

    // Modèle simplifié pour la désérialisation OSRM
    public class GeoJsonRoute
    {
        public Route[] routes { get; set; } = Array.Empty<Route>();
        public class Route
        {
            public double duration { get; set; }
            public double distance { get; set; }
            public Geometry geometry { get; set; } = new Geometry();
        }
        public class Geometry
        {
            public string type { get; set; } = string.Empty;
            public double[][] coordinates { get; set; } = Array.Empty<double[]>();
        }
    }
}
*/