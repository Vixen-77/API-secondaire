using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet("json")]
    public IActionResult GetJson()
    {
        var response = new
        {
            Message = "Hello, yousra!",
            Status = "Success",
            Timestamp = DateTime.UtcNow
        };

        return Ok(response);
    }
}
