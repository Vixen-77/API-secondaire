using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Cors;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{    
    [HttpGet("json")]
    [EnableCors("AllowReactApp")]
    public IActionResult GetJson()
    {
        var response = new
        {
            Message = "Bonjour React! je suis la deusieme API ",
            Status = "Success",
            Timestamp = DateTime.UtcNow
        };

        return Ok(response);
    }
}
