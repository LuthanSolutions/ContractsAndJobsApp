using ContractsAndJobs.Services.ToastService;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Notifications;

namespace ContractsAndJobs.Components.ToastComponent;

public partial class ToastComponent
{
    SfToast? Toast;

    [Inject]
    private IToastService? ToastService { get; set; }

    private bool IsToastVisible { get; set; } = false;

    private ToastOption Options = new ToastOption();

    protected override void OnInitialized()
    {
        ToastService!.ShowToastTrigger += (ToastOption options) =>
        {
            InvokeAsync(() =>
            {
                Options = options;
                IsToastVisible = true;
                StateHasChanged();
                Toast!.ShowAsync();
            });
        };
        base.OnInitialized();
    }
}
