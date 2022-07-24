using ContractsAndJobs.Models;
using ContractsAndJobs.ViewModels;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;

namespace ContractsAndJobs.Pages;

public partial class Index
{
    [Inject]
    private IIndexViewModel? ViewModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await this.ViewModel!.InitialiseViewModelAsync();
    }


    private async Task SelectedPersonChangeHandlerAsync(ChangeEventArgs<string, Contact> args)
    {
        await this.ViewModel!.PopulateContactAsync(args.ItemData.Id);
    }
}