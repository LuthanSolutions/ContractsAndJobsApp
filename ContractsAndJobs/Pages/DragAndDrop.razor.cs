using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ContractsAndJobs.Pages;

public partial class DragAndDrop
{
    [Inject]
    IJSRuntime? JSRuntime { get; set; }

    string message = string.Empty;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await this.JSRuntime!.InvokeVoidAsync("import", "./js/dragdrop.js");
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private void OnButtonClicked(string message)
    {
        this.message = message;
    }
}
