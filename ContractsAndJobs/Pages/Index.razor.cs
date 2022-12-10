using ContractsAndJobs.Models;
using ContractsAndJobs.ViewModels;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Popups;

namespace ContractsAndJobs.Pages;

public partial class Index
{
    SfAutoComplete<string, Contact>? contactSearch;

    [Inject]
    private IIndexViewModel? ViewModel { get; set; }

    [Inject]
    SfDialogService? DialogService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ViewModel!.InitialiseViewModelAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            await contactSearch!.FocusAsync();
        }
    }

    private async Task SelectedPersonChangeHandlerAsync(ChangeEventArgs<string, Contact> args)
    {
        await ViewModel!.PopulateContactAsync(args.ItemData.Id);
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