using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibrarySSMS.Models;
using WEBAPPP.DTO;
using Microsoft.AspNetCore.Cors;
using LibrarySSMS;
using Microsoft.EntityFrameworkCore;
using LibrarySSMS.Enums;

[ApiController]
[Route("api/SVEAmbuController")]
public class SVEAmbuController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<SVEAmbuController> _logger;

    public SVEAmbuController(AppDbContext context, ILogger<SVEAmbuController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpPost("createSVE")]
    [EnableCors("AllowReactApp")]
    public async Task<IActionResult> CreateSVE([FromForm] SVEAmbulanceRequest request)
    {
        var id = Guid.Parse(request.idadminH);
        var centre = Guid.Parse(request.idcentre);
        var vehicule = _context.SVEmbulances.FirstOrDefault(v => v.IdAdminH == id && v.IdCentre == centre);

        if (vehicule == null)
        {
            var vehiculeSVE = new SVEmbulance
            {
                IdAdminH = id,
                IdCentre = centre,
                IdEmbulance = Guid.NewGuid(),
                Matricule = GenerateMatricule(id, 16), // 16 = code wilaya (à rendre dynamique éventuellement)
                isAmbulanceReady = true,
                isAmbulanceAvailable = false,
                isAmbulancePanne = false,
                IsConnected = true,
                Latitude = 0,
                Longitude = 0,
                Timestamp = DateTime.UtcNow,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
            };

            _context.SVEmbulances.Add(vehiculeSVE);
            await _context.SaveChangesAsync();

            return Ok(new{vehiculeSVE.IdEmbulance,vehiculeSVE.isAmbulanceAvailable,vehiculeSVE.isAmbulanceReady,vehiculeSVE.isAmbulancePanne,vehiculeSVE.Matricule});
        }
        else
        {
              vehicule.isAmbulanceAvailable = true;

              vehicule.isAmbulanceReady = true;

              vehicule.isAmbulancePanne = false;

            await _context.SaveChangesAsync();
            return Ok(new { vehicule.IdEmbulance, vehicule.isAmbulanceAvailable, vehicule.isAmbulanceReady, vehicule.isAmbulancePanne, vehicule.Matricule });
        }
    }
[HttpPost("statusSVE")]
[EnableCors("AllowReactApp")]
public async Task<IActionResult> EtatSVE([FromForm] StatusAmbulanceRequest request)
{
    var id = Guid.Parse(request.IdEmbulance);
    var vehicule = await _context.SVEmbulances.FirstOrDefaultAsync(v => v.IdEmbulance == id);

    if (vehicule != null)
    {
        switch (request.StateAmbu)
        {
            case "0":  
                    vehicule.isAmbulanceAvailable = false;
                    vehicule.isAmbulanceReady = false;
                    vehicule.isAmbulancePanne = false;
                    await _context.SaveChangesAsync();
                    return Ok("Ambulance is not available.");
                
            case "1":
              
                    vehicule.isAmbulanceAvailable = true;
                    vehicule.isAmbulanceReady = true;
                    vehicule.isAmbulancePanne = false;
                    await _context.SaveChangesAsync();
                    return Ok("Ambulance is available.");
                
            case "2":
              
                    vehicule.isAmbulancePanne = true;
                    vehicule.isAmbulanceReady = false;
                    vehicule.isAmbulanceAvailable = false;
                    await _context.SaveChangesAsync();
                    return Ok("Ambulance is in breakdown.");
                

            default:
                return BadRequest("Invalid state.");
        }
    }

    
    return NotFound("Ambulance not found.");
}
 
    [HttpPost("ListeSVE")]
    [EnableCors("AllowReactApp")]  
      public async Task<IActionResult> AllAmbulance()
    {
        var ListAmbu = await _context.VehiculeOBUs.ToListAsync();
        return Ok(ListAmbu);

    }  

    private string GenerateMatricule(Guid id, int wilayaCode)
    {
        string idStr = id.ToString("N");
        string partUnique = idStr.Substring(0, 4);
        string numericPart = string.Concat(partUnique.Select(c =>
            char.IsLetter(c) ? ((int)char.ToUpper(c) - 55).ToString() : c.ToString()
        ));
        numericPart = numericPart.Length > 4 ? numericPart.Substring(0, 4) : numericPart.PadRight(4, '0');

        int month = DateTime.UtcNow.Month;
        int year = DateTime.UtcNow.Year % 100;
        string dateCode = $"{month:D2}{year:D2}";


        string wilaya = wilayaCode.ToString("D2");

        return $"{numericPart}-{dateCode}-{wilaya}";
    }
}
