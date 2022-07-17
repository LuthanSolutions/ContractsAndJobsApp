using ContractsAndJobs.ViewModels;
using Microsoft.AspNetCore.Components;

namespace ContractsAndJobs.Pages;

public partial class AddContact
{
    [Inject]
    private IAddContactViewModel? ViewModel { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await this.ViewModel!.InitialiseViewModel();
            this.StateHasChanged();
        }
    }
}