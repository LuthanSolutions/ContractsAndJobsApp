namespace ContractsAndJobs.Components;


using ContractsAndJobs.Models;
using Microsoft.AspNetCore.Components;

public partial class CountriesAndRegions
{
    [Parameter]
    public List<Country>? Countries { get; set; }

    [Parameter]
    public List<Region>? Regions { get; set; }

    private Country? SelectedCountry { get; set; }

    private Region? SelectedRegion { get; set; }

    private List<Region>? RegionsToShow
    {
        get
        {
            return this.SelectedCountry == null ?
                this.Regions :
                this.Regions?.Where(region => region.CountryId == this.SelectedCountry!.Id).ToList();
        }
    }

    private List<Country>? CountriesToShow
    {
        get
        {
            var countriesToShow = this.SelectedRegion == null ?
                this.Countries :
                this.Countries?.Where(country => country.Id == this.SelectedRegion!.CountryId).ToList();
            if (countriesToShow?.Count == 1)
            {
                this.SelectedCountry = countriesToShow[0];
            }
            return countriesToShow;
        }
    }

}
