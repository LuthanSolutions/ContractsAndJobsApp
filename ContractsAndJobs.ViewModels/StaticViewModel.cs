namespace ContractsAndJobs.ViewModels;

public interface IStaticViewModel
{
    string? Message { get; set; }
    delegate void MessageChanged();
}

public class StaticViewModel : IStaticViewModel
{
    public string? Message { get; set; }

    private StaticViewModel(string message)
    {
        this.Message = message;
    }

    private async Task Instantiate(string message)
    {
        await Task.Delay(5000);
        this.Message += message;
    }

    public static async Task<StaticViewModel> Create()
    {
        await Task.Delay(1);
        var instance = new StaticViewModel("Created with constructor");
        var _ = instance.Instantiate(" Added from Instantiate");
        return instance;
    }
}
