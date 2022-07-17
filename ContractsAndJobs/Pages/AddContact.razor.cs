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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await this.ViewModel!.InitialiseViewModel();
            this.StateHasChanged();
        }
    }

    private async Task OnAddClick(MouseEventArgs args)
    {
        await this.ViewModel!.AddContact();
        this.SetDefaultState();
    }

    private async Task OnSaveClick(MouseEventArgs args)
    {
        await this.ViewModel!.UpdateContact();
        this.SetDefaultState();
    }

    private async Task OnDeleteClick(MouseEventArgs args)
    {
        await this.ViewModel!.DeleteContact();
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