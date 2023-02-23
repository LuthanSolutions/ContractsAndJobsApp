using ContractsAndJobs.Data;
using ContractsAndJobs.Models;

namespace ContractsAndJobs.ViewModels;

public interface IAddContactViewModel
{
    Task InitialiseViewModelAsync();
    List<Contact>? Contacts { get; set; }
    Contact? SelectedContact { get; set; }
    Task AddContactAsync();
    Task UpdateContactAsync();
    Task DeleteContactAsync();
}

public class AddContactViewModel : IAddContactViewModel
{
    private readonly IContractsAndJobsDataService contractsAndJobsDataService;

    public List<Contact>? Contacts { get; set; }
    public Contact? SelectedContact { get; set; } = new();

    public AddContactViewModel(IContractsAndJobsDataService contractsAndJobsDataService)
    {
        this.contractsAndJobsDataService = contractsAndJobsDataService;
    }

    public async Task InitialiseViewModelAsync()
    {
        this.Contacts = (await this.contractsAndJobsDataService.GetAllContactsAsync()).ToList();
    }

    public async Task AddContactAsync()
    {
        if (this.SelectedContact != null && (!string.IsNullOrEmpty(this.SelectedContact.FirstName) || !string.IsNullOrEmpty(this.SelectedContact.LastName)))
        {
            await this.contractsAndJobsDataService.AddContactAsync(this.SelectedContact);
            this.Contacts = (await this.contractsAndJobsDataService.GetAllContactsAsync()).ToList();
            this.SelectedContact = new Contact();
        }
    }

    public async Task UpdateContactAsync()
    {
        if (this.SelectedContact is { Id: > 0 } && (!string.IsNullOrEmpty(this.SelectedContact.FirstName) || !string.IsNullOrEmpty(this.SelectedContact.LastName)))
        {
            await this.contractsAndJobsDataService.UpdateContactAsync(this.SelectedContact);
            this.Contacts = (await this.contractsAndJobsDataService.GetAllContactsAsync()).ToList();
            this.SelectedContact = new Contact();
        }
    }

    public async Task DeleteContactAsync()
    {
        if (this.SelectedContact is { Id: > 0 })
        {
            await this.contractsAndJobsDataService.DeleteContactAsync(this.SelectedContact);
            this.Contacts = (await this.contractsAndJobsDataService.GetAllContactsAsync()).ToList();
            this.SelectedContact = new Contact();
        }
    }
}
