using ContractsAndJobs.Models;
using ContractsAndJobs.ViewModels;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;

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

    private void ContactRowSelectHandler(RowSelectEventArgs<Contact> args)
    {
        this.ViewModel!.SelectedContact = args.Data;
    }
}