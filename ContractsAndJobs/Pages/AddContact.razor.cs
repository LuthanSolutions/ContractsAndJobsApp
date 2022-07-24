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
        await this.ViewModel!.InitialiseViewModelAsync();
    }

    private async Task OnAddClickAsync(MouseEventArgs args)
    {
        await this.ViewModel!.AddContactAsync();
        this.SetDefaultState();
    }

    private async Task OnSaveClickAsync(MouseEventArgs args)
    {
        await this.ViewModel!.UpdateContactAsync();
        this.SetDefaultState();
    }

    private async Task OnDeleteClickAsync(MouseEventArgs args)
    {
        await this.ViewModel!.DeleteContactAsync();
        this.SetDefaultState();
    }

    private void OnCancelClick(MouseEventArgs args)
    {
        this.SetDefaultState();
    }

    private void SetDefaultState()
    {
        this.AddDisabled = false;
        this.SaveDisabled = true;
        this.CancelDisabled = true;
        this.DeleteDisabled = true;
        this.ViewModel!.SelectedContact = new Contact();
    }

    private void ContactRowSelectHandler(RowSelectEventArgs<Contact> args)
    {
        this.ViewModel!.SelectedContact = args.Data;
        if (args.Data == null) return;
        this.SaveDisabled = false;
        this.DeleteDisabled = false;
        this.CancelDisabled = false;
    }
}