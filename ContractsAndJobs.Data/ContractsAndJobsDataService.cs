using ContractsAndJobs.Data.DataModels;
using ContractsAndJobs.Models;
using DataServices.Services;
using System.Data;
using System.Data.SqlClient;

namespace ContractsAndJobs.Data;

public interface IContractsAndJobsDataService
{
    Task<IEnumerable<Contact>> GetAllContactsAsync();
    Task<Contact> GetFullContactAsync(int contactId);
    Task AddContactAsync(Contact contact);
    Task UpdateContactAsync(Contact contact);
    Task DeleteContactAsync(Contact contact);
}

public class ContractsAndJobsDataService : IContractsAndJobsDataService
{
    private readonly IDataService dataService;

    private SqlConnection? connection;
    private const string ConnectionString = "data source=LUTHANSOLUTIONS;initial catalog=ContractsAndJobs;trusted_connection=true";

    public ContractsAndJobsDataService(IDataService dataService)
    {
        this.dataService = dataService;
    }

    public async Task<IEnumerable<Contact>> GetAllContactsAsync()
    {
        var contacts = new List<Contact>();
        this.connection = new SqlConnection(ConnectionString);
        await using var command = new SqlCommand();
        command.Connection = this.connection;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetAllContacts";
        await this.connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            contacts.Add(this.dataService.GetObjectFromReader<Contact>(reader));
        }
        return contacts;
    }

    public async Task<Contact> GetFullContactAsync(int contactId)
    {
        var models = new List<ContactDataModel>();
        this.connection = new SqlConnection(ConnectionString);
        await using var command = new SqlCommand();
        command.Connection = this.connection;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "GetFullContactDetails";
        command.Parameters.AddWithValue("@contactId", contactId);
        await this.connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            models.Add(this.dataService.GetObjectFromReader<ContactDataModel>(reader));
        }
        return GetContactFromDataModels(models);
    }

    public async Task AddContactAsync(Contact contact)
    {
        this.connection = new SqlConnection(ConnectionString);
        await using var command = new SqlCommand();
        command.Connection = this.connection;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "AddContact";
        command.Parameters.AddWithValue("@firstName", contact.FirstName);
        command.Parameters.AddWithValue("@lastName", contact.LastName);
        command.Parameters.AddWithValue("@agency", string.IsNullOrEmpty(contact.Agency) ? DBNull.Value : contact.Agency);
        await this.connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        this.connection = new SqlConnection(ConnectionString);
        await using var command = new SqlCommand();
        command.Connection = this.connection;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "UpdateContact";
        command.Parameters.AddWithValue("@id", contact.Id);
        command.Parameters.AddWithValue("@firstName", contact.FirstName);
        command.Parameters.AddWithValue("@lastName", contact.LastName);
        command.Parameters.AddWithValue("@agency", contact.Agency);
        await this.connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteContactAsync(Contact contact)
    {
        this.connection = new SqlConnection(ConnectionString);
        await using var command = new SqlCommand();
        command.Connection = this.connection;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "DeleteContact";
        command.Parameters.AddWithValue("@id", contact.Id);
        await this.connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    private static Contact GetContactFromDataModels(IEnumerable<ContactDataModel> dataModels)
    {
        return dataModels
            .GroupBy(c => c.ContactId)
            .Select(c => new Contact
            {
                Agency = c.First().Agency,
                FirstName = c.First().FirstName,
                LastName = c.First().LastName,
                Id = c.Key!.Value,
                Interactions = c.GroupBy(i => i.InteractionId)
                    .Where(i => i.Key.HasValue)
                    .Select(i => new Interaction
                    {
                        Id = i.Key!.Value,
                        Date = i.First().Date!.Value,
                        Role = new Role
                        {
                            WorkType = (WorkTypes)i.First().WorkType!.Value,
                            Type = (RoleTypes)i.First().Type!.Value,
                            Id = i.First().RoleId!.Value,
                            Company = i.First().Company,
                            DayRate = i.First().DayRate! ?? 0,
                            Salary = i.First().Salary! ?? 0,
                            InsideIr35 = i.First().InsideIr35! ?? false,
                            Location = i.First().Location
                        }
                    })
            }).ToList().First();
    }
}
