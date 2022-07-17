using ContractsAndJobs.Models;
using ContractsAndJobs.ViewModels;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;

namespace ContractsAndJobs.Pages;

public partial class Index
{
    [Inject]
    private IIndexViewModel? ViewModel { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await this.ViewModel!.InitialiseViewModel();
            this.StateHasChanged();
        }
    }

    private async Task SelectedPersonChangeHandler(ChangeEventArgs<string, Contact> args)
    {
        await this.ViewModel!.PopulateContact(args.ItemData.Id);
    }
}