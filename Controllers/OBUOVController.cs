using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibrarySSMS.Models;
using WEBAPPP.DTO;
using Microsoft.AspNetCore.Cors;
using LibrarySSMS;
using Microsoft.EntityFrameworkCore;
using LibrarySSMS.Enums;

[ApiController]
[Route("api/OBUOVController")]

public class OBUController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<OBUController> _logger;

    public OBUController(AppDbContext context, ILogger<OBUController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost("connectOV")]
    [EnableCors("AllowReactApp")]
    public async Task<IActionResult> ConnectOV([FromForm] ConnectionAnoRequest request)

    {
        var idpatient = Guid.Parse(request.idporteur);
        var vehicule = _context.VehiculeOBUs.FirstOrDefault(v => v.idporteur == idpatient);
        if (vehicule == null)
        {
            var vehiculeOBU = new VehiculeOBU
            {
                idporteur = idpatient,
                IdVehiculeOBU = Guid.NewGuid(),
                ADRMAC = GenerateMacAddress(idpatient),
                Marque = "Clio",
                Modele = "Renault",
                IsConnected = true,
                battry = 1,
                typecar = Typecar.OV,
                isRouteur = true, //a retirer
                Latitude = 0,
                Longitude = 0,
                Timestamp = DateTime.UtcNow,
            };
            _context.VehiculeOBUs.Add(vehiculeOBU);
            await _context.SaveChangesAsync();
            Ok(new { vehiculeOBU.ADRMAC, vehiculeOBU.IdVehiculeOBU, vehiculeOBU.IsConnected });
        }

        else

        { Ok(new { vehicule.ADRMAC, vehicule.IdVehiculeOBU, vehicule.IsConnected }); }



        await Task.Delay(100);

        return Ok("Connection successful.");
    }

    [HttpPost("connectSPV")]
    [EnableCors("AllowReactApp")]

    public async Task<IActionResult> ConnectSPV([FromForm] ConnectionAnoRequest request)

    {
        var idpatient = Guid.Parse(request.idporteur);
        var vehicule = _context.VehiculeOBUs.FirstOrDefault(v => v.idporteur == idpatient);
        if (vehicule == null)
        {
            var vehiculeOBU = new VehiculeOBU
            {
                idporteur = idpatient,
                IdVehiculeOBU = Guid.NewGuid(),
                ADRMAC = GenerateMacAddress(idpatient),
                Marque = "Clio",
                Modele = "Renault",
                IsConnected = true,
                battry = 1,
                typecar = Typecar.SPV,
                isRouteur = true, //a retirer
                Latitude = 0,
                Longitude = 0,
                Timestamp = DateTime.UtcNow,
            };
            _context.VehiculeOBUs.Add(vehiculeOBU);
            await _context.SaveChangesAsync();
            Ok(new { vehiculeOBU.ADRMAC, vehiculeOBU.IdVehiculeOBU, vehiculeOBU.IsConnected });
        }

        else

        { Ok(new { vehicule.ADRMAC, vehicule.IdVehiculeOBU, vehicule.IsConnected }); }



        await Task.Delay(100);

        return Ok("Connection successful.");
    }

   private string GenerateMacAddress(Guid id)
    {
        // On utilise le Guid pour générer une pseudo adresse MAC unique
        var bytes = id.ToByteArray();
        return string.Join(":", bytes.Take(6).Select(b => b.ToString("X2")));
    }

}


















