using ContractsAndJobs.Models;
using Microsoft.AspNetCore.Components;

namespace ContractsAndJobs.Components
{
    public partial class CountriesAndRegions
    {
        private Country? SelectedCountry { get; set; }

        private Region? SelectedRegion { get; set; }

        [Parameter]
        public List<Country>? Countries { get; set; }

        [Parameter]
        public List<Region>? Regions { get; set; }

        private List<Region>? RegionsToShow
        {
            get
            {
                return SelectedCountry == null ?
                    Regions :
                    Regions?.Where(region => region.CountryId == SelectedCountry!.Id).ToList();
            }
        }

        private List<Country>? CountriesToShow
        {
            get
            {
                var countriesToShow = SelectedRegion == null ?
                    Countries :
                    Countries?.Where(country => country.Id == SelectedRegion!.CountryId).ToList();
                if(countriesToShow?.Count == 1)
                {
                    SelectedCountry = countriesToShow[0];
                }
                return countriesToShow;
            }
        }

    }
}
