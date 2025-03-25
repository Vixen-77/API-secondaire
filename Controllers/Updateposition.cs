using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
//TODO: ce code sera modifier dans le temps afin de respecter les requetes venu du front 
public class VehicleHub : Hub
{
    private static Dictionary<int, (double lat, double lon)> vehicles = new();
    private static Random random = new();
    private static Timer? _timer;

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("ReceiveMessage", "🟢 Connexion au serveur SignalR établie !");
        
        if (_timer == null)
        {
            _timer = new Timer(UpdateVehiclePositions, null, 0, 1000); // Toutes les secondes
        }
        
        await base.OnConnectedAsync();
    }

    private void UpdateVehiclePositions(object? state)
    {
        foreach (var id in vehicles.Keys)
        {
            vehicles[id] = GenerateRandomPosition();
        }

        Clients.All.SendAsync("ReceiveVehiclePositions", vehicles);
    }

    public async Task RegisterVehicle(int vehicleId)
    {
        if (!vehicles.ContainsKey(vehicleId))
        {
            vehicles[vehicleId] = GenerateRandomPosition();
            await Clients.All.SendAsync("VehicleRegistered", vehicleId);
        }
    }

    private (double lat, double lon) GenerateRandomPosition()
    {
        double lat = 48.8566 + (random.NextDouble() * 0.01); // Simule un déplacement autour de Paris
        double lon = 2.3522 + (random.NextDouble() * 0.01);
        return (lat, lon);
    }
}
