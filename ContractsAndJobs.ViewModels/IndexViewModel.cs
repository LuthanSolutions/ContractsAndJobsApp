using ContractsAndJobs.Data;
using ContractsAndJobs.Models;

namespace ContractsAndJobs.ViewModels
{
    public interface IIndexViewModel
    {
        Task InitialiseViewModel();
        List<Contact>? Contacts { get; set; }
        string? SelectedContact { get; set; }
        Contact? Contact { get; set; }
        Task PopulateContact(int contactId);
    }

    public class IndexViewModel : IIndexViewModel
    {
        private readonly IContractsAndJobsDataService contractsAndJobsDataService;

        public IndexViewModel(IContractsAndJobsDataService contractsAndJobsDataService)
        {
            this.contractsAndJobsDataService = contractsAndJobsDataService;
        }

        public async Task InitialiseViewModel()
        {
            this.Contacts = (await this.contractsAndJobsDataService.GetAllContactsAsync()).ToList();
        }

        public async Task PopulateContact(int contactId)
        {
            this.Contact = await this.contractsAndJobsDataService.GetFullContact(contactId);
        }

        public List<Contact>? Contacts { get; set; }
        public string? SelectedContact { get; set; }
        public Contact? Contact { get; set; }
    }
}