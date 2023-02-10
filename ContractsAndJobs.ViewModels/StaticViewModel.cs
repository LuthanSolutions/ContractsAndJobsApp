namespace ContractsAndJobs.ViewModels
{
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
            Message = message;
        }

        private async Task Instantiate(string message)
        {
            await Task.Delay(5000);
            Message += message;
        }

        public static async Task<StaticViewModel> Create()
        {
            var instance = new StaticViewModel("Created with constructor");
            var _ = instance.Instantiate(" Added from Instantiate");
            return instance;
        }
    }
}
