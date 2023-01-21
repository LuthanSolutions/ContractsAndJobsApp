using ContractsAndJobs.ViewModels.InstantiateAsyncViewModels;
using Microsoft.AspNetCore.Components;

namespace ContractsAndJobs.Pages
{
    public partial class InstantiateAsync
    {
        [Inject]
        public IInstantiateAsyncPeopleViewModel? ViewModel { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ViewModel!.InstantiateAsync();
        }
    }
}
