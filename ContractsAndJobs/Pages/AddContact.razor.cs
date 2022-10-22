using ContractsAndJobs.Models;
using ContractsAndJobs.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Grids;

namespace ContractsAndJobs.Pages;

public partial class AddContact
{
    [Inject]
    private IAddContactViewModel? ViewModel { get; set; }

    private bool AddDisabled { get; set; }
    private bool SaveDisabled { get; set; } = true;
    private bool CancelDisabled { get; set; } = true;
    private bool DeleteDisabled { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        await ViewModel!.InitialiseViewModelAsync();
    }

    private async Task OnAddClickAsync(MouseEventArgs args)
    {
        await ViewModel!.AddContactAsync();
        SetDefaultState();
    }

    private async Task OnSaveClickAsync(MouseEventArgs args)
    {
        await ViewModel!.UpdateContactAsync();
        SetDefaultState();
    }

    private async Task OnDeleteClickAsync(MouseEventArgs args)
    {
        await ViewModel!.DeleteContactAsync();
        SetDefaultState();
    }

    private void OnCancelClick(MouseEventArgs args)
    {
        SetDefaultState();
    }

    private void SetDefaultState()
    {
        AddDisabled = false;
        SaveDisabled = true;
        CancelDisabled = true;
        DeleteDisabled = true;
        ViewModel!.SelectedContact = new Contact();
    }

    private void ContactRowSelectHandler(RowSelectEventArgs<Contact> args)
    {
        ViewModel!.SelectedContact = args.Data;
        if (args.Data == null) return;
        SaveDisabled = false;
        DeleteDisabled = false;
        CancelDisabled = false;
    }
}