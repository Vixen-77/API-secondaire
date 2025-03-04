using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WEBAPPP.Models;
using System.Collections.Generic;

public class TestService
{
    private readonly HttpClient _httpClient;

    public TestService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Test>> GetTestsFromSecondaryAPI()
    {
        var response = await _httpClient.GetAsync("http://localhost:5001/api/test"); // Remplace avec l'URL de l'API secondaire

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Test>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Test>();
        }

        return  new List<Test>();
    }
}
