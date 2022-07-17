using ContractsAndJobs.Data;
using ContractsAndJobs.Models;

namespace ContractsAndJobs.ViewModels
{
    public interface IAddContactViewModel
    {
        Task InitialiseViewModel();
        List<Contact>? Contacts { get; set; }
        Contact? SelectedContact { get; set; }
    }

    public class AddContactViewModel : IAddContactViewModel
    {
        private readonly IContractsAndJobsDataService contractsAndJobsDataService;

        public List<Contact>? Contacts { get; set; }
        public Contact? SelectedContact { get; set; } = new ();

        public AddContactViewModel(IContractsAndJobsDataService contractsAndJobsDataService)
        {
            this.contractsAndJobsDataService = contractsAndJobsDataService;
        }

        public async Task InitialiseViewModel()
        {
            this.Contacts = (await this.contractsAndJobsDataService.GetAllContactsAsync()).ToList();
        }
    }
}
