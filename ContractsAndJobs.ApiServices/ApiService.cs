using ContractsAndJobs.Models;
using System.Net.Http.Json;

namespace ContractsAndJobs.ApiServices;

public interface IApiService
{
    Task<Country> GetCountry(string countryName);
}

public class ApiService : IApiService
{
    private readonly HttpClient httpClient;

    public ApiService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Country> GetCountry(string countryName)
    {
        var val = await this.httpClient.GetFromJsonAsync<dynamic>("London");
        return new Country();
    }
}
