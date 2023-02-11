using Syncfusion.Blazor.Popups;

namespace ContractsAndJobs.Services;

public interface IDialogService
{
    Task AlertAsync(string title, string message, string? buttonText = null);
    Task<bool> ConfirmAsync(string title, string message, string? primaryButtonText = null, string? secondaryButtonText = null);
    Task<string> PromptAsync(string title, string message, string? primaryButtonText = null, string? secondaryButtonText = null);
}

public class DialogService : IDialogService
{
    private readonly SfDialogService dialogService;

    private readonly DialogOptions dialogOptions = new()
    {
        AllowDragging = true,
        ShowCloseIcon = true,
        AnimationSettings = new() { Delay = 0, Duration = 250, Effect = DialogEffect.FadeZoom },
        Position = new() { X = "center", Y = "center" },
        CssClass = "ModalDialog"
    };

    public DialogService(SfDialogService dialogService)
    {
        this.dialogService = dialogService;
    }

    public async Task AlertAsync(string title, string message, string? buttonText = null)
    {
        SetButtonTexts(buttonText);

        await dialogService.AlertAsync(message, title, dialogOptions);

        ClearButtonTexts();
    }

    public async Task<bool> ConfirmAsync(string title, string message, string? primaryButtonText = null, string? secondaryButtonText = null)
    {
        SetButtonTexts(primaryButtonText, secondaryButtonText);

        var result = await dialogService.ConfirmAsync(message, title, dialogOptions);

        ClearButtonTexts();

        return result;
    }

    public async Task<string> PromptAsync(string title, string message, string? primaryButtonText = null, string? secondaryButtonText = null)
    {
        SetButtonTexts(primaryButtonText, secondaryButtonText);

        var result = await dialogService.PromptAsync(message, title, dialogOptions);

        ClearButtonTexts();

        return result;
    }

    private void SetButtonTexts(string? primaryButtonText = null, string? secondaryButtonText = null)
    {
        if (!string.IsNullOrEmpty(primaryButtonText))
        {
            dialogOptions.PrimaryButtonOptions = new DialogButtonOptions { Content = primaryButtonText };
        }
        if (!string.IsNullOrEmpty(secondaryButtonText))
        {
            dialogOptions.CancelButtonOptions = new DialogButtonOptions { Content = secondaryButtonText };
        }
    }

    private void ClearButtonTexts()
    {
        dialogOptions.PrimaryButtonOptions = null;
        dialogOptions.CancelButtonOptions = null;
    }
}
