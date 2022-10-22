using ContractsAndJobs.Data;
using ContractsAndJobs.Models;

namespace ContractsAndJobs.ViewModels
{
    public interface IIndexViewModel
    {
        Task InitialiseViewModelAsync();
        List<Contact>? Contacts { get; set; }
        string? SelectedContact { get; set; }
        Contact? Contact { get; set; }
        Task PopulateContactAsync(int contactId);
    }

    public class IndexViewModel : IIndexViewModel
    {
        private readonly IContractsAndJobsDataService contractsAndJobsDataService;

        public IndexViewModel(IContractsAndJobsDataService contractsAndJobsDataService)
        {
            this.contractsAndJobsDataService = contractsAndJobsDataService;
        }

        public async Task InitialiseViewModelAsync()
        {
            Contacts = (await contractsAndJobsDataService.GetAllContactsAsync()).ToList();
        }

        public async Task PopulateContactAsync(int contactId)
        {
            Contact = await contractsAndJobsDataService.GetFullContactAsync(contactId);
        }

        public List<Contact>? Contacts { get; set; }
        public string? SelectedContact { get; set; }
        public Contact? Contact { get; set; }
    }
}