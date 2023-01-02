using ContractsAndJobs.Data;
using ContractsAndJobs.Models;

namespace ContractsAndJobs.ViewModels.InstantiateAsyncViewModels
{

    public interface IInstantiateAsyncPeopleViewModel : IInstantiateAsyncViewModel
    {
        IEnumerable<Contact>? People { get; set; }
    }

    public class InstantiateAsyncPeopleViewModel : IInstantiateAsyncPeopleViewModel
    {
        private readonly IContractsAndJobsDataService contractsAndJobsDataService;

        public InstantiateAsyncPeopleViewModel(IContractsAndJobsDataService contractsAndJobsDataService)
        {
            this.contractsAndJobsDataService = contractsAndJobsDataService;
        }

        public async Task InstantiateAsync()
        {
            People ??= await contractsAndJobsDataService.GetAllContactsAsync();
        }

        public IEnumerable<Contact>? People { get; set; }
    }
}
