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
            await this.JSRuntime!.InvokeVoidAsync("signalDragEnd", DotNetObjectReference.Create(this));
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    [JSInvokable("CallStateHasChanged")]
    public async Task CallStateHasChanged()
    {
        await InvokeAsync(StateHasChanged);
    }

    private void OnButtonClicked(string message)
    {
        this.message = message;
    }
}
