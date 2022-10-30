using ContractsAndJobs.Models;
using ContractsAndJobs.ViewModels;
using Microsoft.AspNetCore.Components;

namespace ContractsAndJobs.Pages
{
    public partial class LazyViewModel
    {
        [Inject]
        private ILazyViewModel? ViewModel { get; set; }

        private List<Person>? People = null;


        protected override async Task OnParametersSetAsync()
        {
            this.People = await this.ViewModel!.People;
        }
    }
}
