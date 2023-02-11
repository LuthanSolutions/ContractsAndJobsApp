namespace ContractsAndJobs.Services.ToastService;

public interface IToastService
{
    event Action<ToastOption>? ShowToastTrigger;

    void ShowToast(ToastOption options);
}

public class ToastService : IToastService
{
    public event Action<ToastOption>? ShowToastTrigger;
    public void ShowToast(ToastOption options)
    {
        ShowToastTrigger!.Invoke(options);
    }
}
