using ContractsAndJobs.Models;
using ContractsAndJobs.ViewModels;
using Microsoft.AspNetCore.Components;

namespace ContractsAndJobs.Pages
{
    public partial class LazyViewModel
    {
        [Inject]
        private ILazyViewModel? ViewModel { get; set; }

        private List<Contact>? Contacts = null;


        protected override async Task OnParametersSetAsync()
        {
            Contacts = await ViewModel!.Contacts;
        }
    }
}
