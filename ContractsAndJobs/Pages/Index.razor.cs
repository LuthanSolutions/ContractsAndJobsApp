using ContractsAndJobs.Models;
using ContractsAndJobs.ViewModels;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Popups;

namespace ContractsAndJobs.Pages;

public partial class Index
{
    [Inject]
    private IIndexViewModel? ViewModel { get; set; }

    [Inject]
    SfDialogService? DialogService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await this.ViewModel!.InitialiseViewModelAsync();
    }

    private async Task SelectedPersonChangeHandlerAsync(ChangeEventArgs<string, Contact> args)
    {
        await this.ViewModel!.PopulateContactAsync(args.ItemData.Id);
    }

    private async Task ShowAlert() => 
        await DialogService!.AlertAsync(
            "This is the alert content", 
            "Title",
            new DialogOptions
            {
                Width = "20rem",
                Height = "15rem",
                CssClass = "customClass "
            });
}