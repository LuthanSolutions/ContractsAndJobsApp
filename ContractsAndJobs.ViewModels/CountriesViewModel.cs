using ContractsAndJobs.Models;
using ContractsAndJobs.Services;

namespace ContractsAndJobs.ViewModels;

public interface ICountriesViewModel : IInstantiateAsyncViewModel
{
    IEnumerable<Country>? Countries { get; set; }
    Country? SelectedCountry { get; set; }
}

public class CountriesViewModel : ICountriesViewModel
{
    private readonly ICountriesService countriesService;

    public CountriesViewModel(ICountriesService countriesService)
    {
        this.countriesService = countriesService;
    }

    public IEnumerable<Country>? Countries { get; set; }
    public Country? SelectedCountry { get; set; }

    public async Task InstantiateAsync()
    {
        this.Countries = await this.countriesService.GetCountriesAsync();
    }
}
