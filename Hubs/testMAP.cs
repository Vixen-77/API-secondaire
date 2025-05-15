using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace WEBAPPP.Hubs
{
    public class AmbulanceHub : Hub
    {
        private static readonly Random _rnd = new();
        private static readonly (double lat, double lon) _centre = (36.7400, 2.9510);
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AmbulanceHub(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var simId = httpContext?.Request.Query["simId"].ToString();

            if (!string.IsNullOrEmpty(simId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, simId);
            }
            else
            {
                throw new InvalidOperationException("Simulation ID is missing or invalid.");
            }

            await base.OnConnectedAsync();
        }

        public async Task StartSimulation(string simulationId)
        {
            var randomPoint = GenerateRandomPoint();
            await RunRoute(simulationId, _centre, randomPoint);
            await RunRoute(simulationId, randomPoint, _centre);
        }

        public async Task ChangeDestination(string simulationId)
        {
            await RunRoute(simulationId, _centre, GenerateRandomPoint());
        }

        private async Task RunRoute(string group, (double lat, double lon) start, (double lat, double lon) end)
        {
            var apiKey = "5b3ce3597851110001cf6248847ab97b259f44838961bf30a33d1314";
            var url = "https://api.openrouteservice.org/v2/directions/driving-car";

            var requestBody = new
            {
                coordinates = new[]
                {
                    new[] { start.lon, start.lat },
                    new[] { end.lon, end.lat }
                }
            };

            var jsonBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erreur OpenRouteService : {error}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Réponse ORS: {jsonResponse}");

            var document = JsonDocument.Parse(jsonResponse);
            if (!document.RootElement.TryGetProperty("routes", out var routes) || routes.GetArrayLength() == 0)
                throw new Exception("La réponse de l'API ne contient aucun 'routes'.");

            var route = routes[0];

            if (!route.TryGetProperty("geometry", out var geometryElement))
                throw new Exception("La réponse de l'API ne contient pas 'geometry'.");

            var polyline = geometryElement.GetString();
            if (string.IsNullOrEmpty(polyline))
            {
                throw new ArgumentException("Polyline cannot be null or empty.", nameof(polyline));
            }

            var coordinates = DecodePolyline(polyline);

            foreach (var (lon, lat) in coordinates)
            {
                await Clients.Group(group).SendAsync("ReceivePosition", new { lat, lon });
                await Task.Delay(200); // 200ms entre chaque point
            }
        }

        private (double lat, double lon) GenerateRandomPoint()
        {
            double radiusKm = 3;
            double u = _rnd.NextDouble();
            double v = _rnd.NextDouble();
            double w = radiusKm / 6371.0;
            double brg = 2 * Math.PI * v;
            double d = Math.Acos(1 - u * (1 - Math.Cos(w)));

            double lat1 = ToRadians(_centre.lat);
            double lon1 = ToRadians(_centre.lon);
            double lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(d) + Math.Cos(lat1) * Math.Sin(d) * Math.Cos(brg));
            double lon2 = lon1 + Math.Atan2(Math.Sin(brg) * Math.Sin(d) * Math.Cos(lat1), Math.Cos(d) - Math.Sin(lat1) * Math.Sin(lat2));

            return (ToDegrees(lat2), ToDegrees(lon2));
        }

        private static double ToRadians(double deg) => deg * Math.PI / 180;
        private static double ToDegrees(double rad) => rad * 180 / Math.PI;

        // Décode une polyligne encodée (encodage Google)
        private static List<(double lon, double lat)> DecodePolyline(string polyline)
        {
            var coordinates = new List<(double lon, double lat)>();
            int index = 0, len = polyline.Length;
            int lat = 0, lon = 0;

            while (index < len)
            {
                int result = 1, shift = 0, b;
                do
                {
                    b = polyline[index++] - 63 - 1;
                    result += b << shift;
                    shift += 5;
                } while (b >= 0x1f);
                lat += (result & 1) != 0 ? ~(result >> 1) : (result >> 1);

                result = 1;
                shift = 0;
                do
                {
                    b = polyline[index++] - 63 - 1;
                    result += b << shift;
                    shift += 5;
                } while (b >= 0x1f);
                lon += (result & 1) != 0 ? ~(result >> 1) : (result >> 1);

                coordinates.Add((lon / 1E5, lat / 1E5));
            }

            return coordinates;
        }
    }
}




// AmbulanceHub.cs
/*using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace WEBAPPPP.Hubs
{
    public class AmbulanceHub : Hub
    {
        private static readonly Random _rnd = new();
        private static readonly (double lat, double lon) _centre = (36.7400, 2.9510);
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AmbulanceHub(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var simId = httpContext?.Request.Query["simId"].ToString();

            if (!string.IsNullOrEmpty(simId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, simId);
            }
            else
            {
                throw new InvalidOperationException("Simulation ID is missing or invalid.");
            }
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Démarre la simulation aller-retour
        /// </summary>
        public async Task StartSimulation(string simulationId)
        {
            var randomPoint = GenerateRandomPoint();
            await RunRoute(simulationId, _centre, randomPoint);
            await RunRoute(simulationId, randomPoint, _centre);
        }

        /// <summary>
        /// Change la destination en plein vol
        /// </summary>
        public async Task ChangeDestination(string simulationId)
        {
            await RunRoute(simulationId, _centre, GenerateRandomPoint());
        }

        private async Task RunRoute(string group, (double lat, double lon) start, (double lat, double lon) end)
        {
            var apiKey = "5b3ce3597851110001cf6248847ab97b259f44838961bf30a33d1314";
            var url = "https://api.openrouteservice.org/v2/directions/driving-car";

            var requestBody = new
            {
                coordinates = new[]
                {
                    new[] { start.lon, start.lat },
                    new[] { end.lon, end.lat }
                }
            };

            var jsonBody = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erreur OpenRouteService : {error}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(jsonResponse);

            var coordinates = document.RootElement
                .GetProperty("features")[0]
                .GetProperty("geometry")
                .GetProperty("coordinates");

            foreach (var point in coordinates.EnumerateArray())
            {
                var lon = point[0].GetDouble();
                var lat = point[1].GetDouble();
                await Clients.Group(group).SendAsync("ReceivePosition", new { lat, lon });
                await Task.Delay(200); // 200ms entre chaque point
            }
        }

        private (double lat, double lon) GenerateRandomPoint()
        {
            double radiusKm = 3;
            double u = _rnd.NextDouble();
            double v = _rnd.NextDouble();
            double w = radiusKm / 6371.0;
            double brg = 2 * Math.PI * v;
            double d = Math.Acos(1 - u * (1 - Math.Cos(w)));

            double lat1 = ToRadians(_centre.lat);
            double lon1 = ToRadians(_centre.lon);
            double lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(d) + Math.Cos(lat1) * Math.Sin(d) * Math.Cos(brg));
            double lon2 = lon1 + Math.Atan2(Math.Sin(brg) * Math.Sin(d) * Math.Cos(lat1), Math.Cos(d) - Math.Sin(lat1) * Math.Sin(lat2));

            return (ToDegrees(lat2), ToDegrees(lon2));
        }

        private static double ToRadians(double deg) => deg * Math.PI / 180;
        private static double ToDegrees(double rad) => rad * 180 / Math.PI;
    }
}*/

















// AmbulanceHub.cs
/*
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WEBAPPPP.Hubs
{
    public class AmbulanceHub : Hub
    {
        private static readonly Random _rnd = new();
        private static readonly (double lat, double lon) _centre = (36.7400, 2.9510);

        public override async Task OnConnectedAsync()
        {
            // Récupère l'ID de simulation depuis la query string et ajoute le client au groupe
            var httpContext = Context.GetHttpContext();
            var simId = httpContext?.Request.Query["simId"].ToString();

            if (!string.IsNullOrEmpty(simId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, simId);
            }
            else
            {
                throw new InvalidOperationException("Simulation ID is missing or invalid.");
            }
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Démarre la simulation aller-retour
        /// </summary>
        public async Task StartSimulation(string simulationId)
        {
            await RunRoute(simulationId, _centre, GenerateRandomPoint());
            await RunRoute(simulationId, GenerateRandomPoint(), _centre);
        }

        /// <summary>
        /// Change la destination en plein vol
        /// </summary>
        public async Task ChangeDestination(string simulationId)
        {
            // On considère que le client garde en mémoire sa dernière position
            // Ici on repart du centre pour simplifier
            await RunRoute(simulationId, _centre, GenerateRandomPoint());
        }

        private async Task RunRoute(string group, (double lat, double lon) start, (double lat, double lon) end)
        {
            // Simpler route: une ligne droite pour l’exemple
            int steps = 50;
            for (int i = 1; i <= steps; i++)
            {
                var lat = start.lat + (end.lat - start.lat) * i / steps;
                var lon = start.lon + (end.lon - start.lon) * i / steps;
                await Clients.Group(group).SendAsync("ReceivePosition", new { lat, lon });
                await Task.Delay(200);
            }
        }

        private (double lat, double lon) GenerateRandomPoint()
        {
            // Rayon de 3000m
            double radiusKm = 3;
            double u = _rnd.NextDouble();
            double v = _rnd.NextDouble();
            double w = radiusKm / 6371.0;
            double brg = 2 * Math.PI * v;
            double d = Math.Acos(1 - u * (1 - Math.Cos(w)));

            double lat1 = ToRadians(_centre.lat);
            double lon1 = ToRadians(_centre.lon);
            double lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(d) + Math.Cos(lat1) * Math.Sin(d) * Math.Cos(brg));
            double lon2 = lon1 + Math.Atan2(Math.Sin(brg) * Math.Sin(d) * Math.Cos(lat1), Math.Cos(d) - Math.Sin(lat1) * Math.Sin(lat2));

            return (ToDegrees(lat2), ToDegrees(lon2));
        }

        private static double ToRadians(double deg) => deg * Math.PI / 180;
        private static double ToDegrees(double rad) => rad * 180 / Math.PI;
    }
}*/