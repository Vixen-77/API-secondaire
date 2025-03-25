using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("api/test2")]
public class FindpatieninAPI1 : ControllerBase
{
    private readonly HttpClient _httpClient;

    public FindpatieninAPI1(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiPatients"); // Utilisation d'un client nommé
    }

    [HttpGet("serialize")]
    public async Task<IActionResult> Serialize([FromQuery] bool isSender = true)
    {
        if (isSender)
        {
            // Mode "Expéditeur" -> Appel à l'autre API
            var response = await _httpClient.GetAsync("/api/test2/serialize?isSender=false");

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
