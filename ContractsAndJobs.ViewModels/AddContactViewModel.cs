using ContractsAndJobs.Data;
using ContractsAndJobs.Models;

namespace ContractsAndJobs.ViewModels
{
    public interface IAddContactViewModel
    {
        Task InitialiseViewModel();
        List<Contact>? Contacts { get; set; }
        Contact? SelectedContact { get; set; }
        Task AddContact();
        Task UpdateContact();
        Task DeleteContact();
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

        public async Task AddContact()
        {
            if (this.SelectedContact != null && (!string.IsNullOrEmpty(this.SelectedContact.FirstName) || !string.IsNullOrEmpty(this.SelectedContact.LastName)))
            {
                await this.contractsAndJobsDataService.AddContact(this.SelectedContact);
                this.Contacts = (await this.contractsAndJobsDataService.GetAllContactsAsync()).ToList();
                this.SelectedContact = new Contact();
            }
        }

        public async Task UpdateContact()
        {
            if (this.SelectedContact is {Id: > 0} && (!string.IsNullOrEmpty(this.SelectedContact.FirstName) || !string.IsNullOrEmpty(this.SelectedContact.LastName)))
            {
                await this.contractsAndJobsDataService.UpdateContact(this.SelectedContact);
                this.Contacts = (await this.contractsAndJobsDataService.GetAllContactsAsync()).ToList();
                this.SelectedContact = new Contact();
            }
        }

        public async Task DeleteContact()
        {
            if (this.SelectedContact is {Id: > 0})
            {
                await this.contractsAndJobsDataService.DeleteContact(this.SelectedContact);
                this.Contacts = (await this.contractsAndJobsDataService.GetAllContactsAsync()).ToList();
                this.SelectedContact = new Contact();
            }
        }
    }
}
