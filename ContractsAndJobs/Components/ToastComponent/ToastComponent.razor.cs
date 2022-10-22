using ContractsAndJobs.Services.ToastService;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Notifications;

namespace ContractsAndJobs.Components.ToastComponent
{
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
                    this.Options = options;
                    this.IsToastVisible = true;
                    this.StateHasChanged();
                    this.Toast!.ShowAsync();
                });
            };
            base.OnInitialized();
        }
    }
}
