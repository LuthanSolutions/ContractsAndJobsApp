using ContractsAndJobs.Models;
using ContractsAndJobs.RepositroyServices;

namespace ContractsAndJobs.Services;

public interface ICountriesService
{
    Task<IEnumerable<Country>?> GetCountriesAsync();
}

public class CountriesService : ICountriesService
{
    private readonly ICountriesRepositoryService countriesRepositoryService;

    public CountriesService(ICountriesRepositoryService countriesRepositoryService)
    {
        this.countriesRepositoryService = countriesRepositoryService;
    }

    public async Task<IEnumerable<Country>?> GetCountriesAsync()
    {
        return await this.countriesRepositoryService.GetAllCountriesAsync();
    }
}
