using Microsoft.JSInterop;

namespace ContractsAndJobs.Services;

public interface IBrowserService
{
    public Task ShowAlertMessage(string message);

    public Task<bool> GetConfirmation(string message);

    public Task<string> GetUserInput(string message);
}

public class BrowserService : IBrowserService
{
    private const string AlertCommand = "alert";
    private const string ConfirmCommand = "confirm";
    private const string PromptCommand = "prompt";

    private readonly IJSRuntime? jsRuntime;

    public BrowserService(IJSRuntime? jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }

    public async Task ShowAlertMessage(string message) =>
        await this.jsRuntime!.InvokeVoidAsync(AlertCommand, message);

    public async Task<bool> GetConfirmation(string message) =>
        await this.jsRuntime!.InvokeAsync<bool>(ConfirmCommand, message);

    public async Task<string> GetUserInput(string message) =>
        await this.jsRuntime!.InvokeAsync<string>(PromptCommand, message);
}