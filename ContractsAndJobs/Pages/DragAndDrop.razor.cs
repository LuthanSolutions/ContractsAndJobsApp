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

    private string? startId { get; set; }
    private string? stopId { get; set; }

    [JSInvokable("DragStarted")]
    public async Task DragStarted(string target)
    {
        startId = target;
    }

    [JSInvokable("ItemDropped")]
    public async Task ItemDropped(string target)
    {
        stopId = target;
    }

    private void OnButtonClicked(string message)
    {
        this.message = message;
    }
}
