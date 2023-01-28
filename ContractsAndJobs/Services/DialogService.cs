using Syncfusion.Blazor.Popups;

namespace ContractsAndJobs.Services
{
    public interface IDialogService
    {
        Task AlertAsync(string title, string message, string? primaryButtonText = null);
        Task<bool> ConfirmAsync(string title, string message, string? primaryButtonText = null);
        Task<string> PromptAsync(string title, string message, string? primaryButtonText = null);
    }

    public class DialogService : IDialogService
    {
        private readonly SfDialogService dialogService;

        private readonly DialogOptions dialogOptions = new DialogOptions
        {
            AllowDragging = true,
            ShowCloseIcon = true,
            AnimationSettings = new DialogAnimationOptions { Delay = 0, Duration = 250, Effect = DialogEffect.FadeZoom },
            Position = new PositionDataModel { X = "center", Y = "center" },
            CssClass = "ModalDialog"
        };

        public DialogService(SfDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        public async Task AlertAsync(string title, string message, string? primaryButtonText = null)
        {
            if (!string.IsNullOrEmpty(primaryButtonText))
            {
                dialogOptions.PrimaryButtonOptions = new DialogButtonOptions { Content = primaryButtonText };
            }

            await dialogService.AlertAsync(message, title, dialogOptions);

            dialogOptions.PrimaryButtonOptions = null;
        }

        public async Task<bool> ConfirmAsync(string title, string message, string? primaryButtonText = null)
        {
            if (!string.IsNullOrEmpty(primaryButtonText))
            {
                dialogOptions.PrimaryButtonOptions = new DialogButtonOptions { Content = primaryButtonText };
            }

            var result = await dialogService.ConfirmAsync(message, title, dialogOptions);

            dialogOptions.PrimaryButtonOptions = null;

            return result;
        }

        public async Task<string> PromptAsync(string title, string message, string? primaryButtonText = null)
        {
            if (!string.IsNullOrEmpty(primaryButtonText))
            {
                dialogOptions.PrimaryButtonOptions = new DialogButtonOptions { Content = primaryButtonText };
            }

            var result = await dialogService.PromptAsync(message, title, dialogOptions);

            dialogOptions.PrimaryButtonOptions = null;

            return result;
        }
    }
}
