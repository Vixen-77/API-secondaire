using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

[ApiController]
[Route("api/test2")]  // Route principale de l'API B
public class TestC : ControllerBase
{
  private readonly HttpClient _httpClient;

 public TestC(IHttpClientFactory httpClientFactory)
{
    _httpClient = httpClientFactory.CreateClient();
}
    
    [HttpGet("serialize")]

    public async Task<IActionResult> Serialize([FromQuery] bool isSender = true){
    if (isSender)
    {
        // Mode "Expéditeur" -> Envoie la requête à TestC
        var response = await _httpClient.GetAsync("http://localhost:5001/api/test2/serialize?isSender=false");

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, "Erreur lors de l'appel à l'API principale.");
        }

        string result = await response.Content.ReadAsStringAsync();
        return Ok($"Réponse de l'API B : {result}");
    }
    else
    {
        // Mode "Récepteur" -> Retourne une simple réponse
        return Ok("Réponse de l'API secondaire !");
    }

    }
}
