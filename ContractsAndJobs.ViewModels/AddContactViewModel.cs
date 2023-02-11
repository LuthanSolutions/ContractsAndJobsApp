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
        Contacts = (await contractsAndJobsDataService.GetAllContactsAsync()).ToList();
    }

    public async Task AddContactAsync()
    {
        if (SelectedContact != null && (!string.IsNullOrEmpty(SelectedContact.FirstName) || !string.IsNullOrEmpty(SelectedContact.LastName)))
        {
            await contractsAndJobsDataService.AddContactAsync(SelectedContact);
            Contacts = (await contractsAndJobsDataService.GetAllContactsAsync()).ToList();
            SelectedContact = new Contact();
        }
    }

    public async Task UpdateContactAsync()
    {
        if (SelectedContact is { Id: > 0 } && (!string.IsNullOrEmpty(SelectedContact.FirstName) || !string.IsNullOrEmpty(SelectedContact.LastName)))
        {
            await contractsAndJobsDataService.UpdateContactAsync(SelectedContact);
            Contacts = (await contractsAndJobsDataService.GetAllContactsAsync()).ToList();
            SelectedContact = new Contact();
        }
    }

    public async Task DeleteContactAsync()
    {
        if (SelectedContact is { Id: > 0 })
        {
            await contractsAndJobsDataService.DeleteContactAsync(SelectedContact);
            Contacts = (await contractsAndJobsDataService.GetAllContactsAsync()).ToList();
            SelectedContact = new Contact();
        }
    }
}
