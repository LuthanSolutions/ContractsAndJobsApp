using ContractsAndJobs.ViewModels;
using Microsoft.AspNetCore.Components;

namespace ContractsAndJobs.Pages
{
    public partial class StaticViewModel
    {
        [Inject]
        private IStaticViewModel? ViewModel { get; set; }

    }
}
