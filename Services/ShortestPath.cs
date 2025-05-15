/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public interface IDispatchService
{
    Task DispatchNearestVehiclesAsync(double patientLat, double patientLng, List<Vehicle> vehicles);
}

public class DispatchService : IDispatchService
{
    // Le hub SignalR servant à envoyer les notifications
    private readonly IHubContext<NotificationHub> _hubContext;

    public DispatchService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    // Méthode pour calculer la distance entre deux coordonnées en km (formule de Haversine)
    public double CalculateDistance(double lat1, double lng1, double lat2, double lng2)
    {
        const double R = 6371; // Rayon de la Terre en km
        var dLat = DegreesToRadians(lat2 - lat1);
        var dLng = DegreesToRadians(lng2 - lng1);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                Math.Sin(dLng / 2) * Math.Sin(dLng / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    private double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }

    // Méthode qui trouve les 3 véhicules les plus proches et notifie chacun via SignalR
    public async Task DispatchNearestVehiclesAsync(double patientLat, double patientLng, List<Vehicle> vehicles)
    {
        // Calculer la distance de chaque véhicule par rapport à la position du patient
        var vehiclesWithDistance = vehicles.Select(v => new
        {
            Vehicle = v,
            Distance = CalculateDistance(patientLat, patientLng, v.Latitude, v.Longitude)
        });

        // Ordonner par distance croissante et prendre les 3 premiers
        var closestVehicles = vehiclesWithDistance.OrderBy(v => v.Distance).Take(3).ToList();

        // Envoyer une notification à chacun via SignalR
        foreach (var item in closestVehicles)
        {
            // Le payload peut contenir l'ID du véhicule, la distance calculée et tout autre champ utile.
            await _hubContext.Clients.Client(item.Vehicle.Id)
                .SendAsync("DispatchNotification", new
                {
                    PatientPosition = new { Latitude = patientLat, Longitude = patientLng },
                    VehiclePosition = new { Latitude = item.Vehicle.Latitude, Longitude = item.Vehicle.Longitude },
                    Distance = item.Distance,
                    Message = "Une anomalie a été détectée, vous êtes sélectionné pour une prise en charge."
                });
        }
    }
}
*/