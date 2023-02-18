using ContractsAndJobs.ViewModels;
using Microsoft.AspNetCore.Components;

namespace ContractsAndJobs.Pages;

public partial class Countries
{
    [Inject]
    private ICountriesViewModel? ViewModel { get; set; }

    protected async override Task OnInitializedAsync()
    {
        await ViewModel!.InstantiateAsync();
    }
}
