using ContractsAndJobs.Data.DataModels;
using ContractsAndJobs.Models;
using Dapper;
using System.Data.SqlClient;

namespace ContractsAndJobs.Data;

public class DapperDataService : IContractsAndJobsDataService
{
    private const string ConnectionString = "data source=LUTHANSOLUTIONS;initial catalog=ContractsAndJobs;trusted_connection=true";
    private const string GetAllContactsSprocName = "GetAllContacts";
    private const string GetFullContactDetailsSprocName = "GetFullContactDetails";
    private const string AddContactSprocName = "AddContact";
    private const string UpdateContactSprocName = "UpdateContact";
    private const string DeleteContactSprocName = "DeleteContact";

    public async Task AddContactAsync(Contact contact)
    {
        using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("firstName", contact.FirstName);
        parameters.Add("lastName", contact.LastName);
        parameters.Add("agency", contact.Agency);
        await connection.ExecuteAsync(
            AddContactSprocName, 
            parameters, 
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task DeleteContactAsync(Contact contact)
    {
        using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("id", contact.Id);
        await connection.ExecuteAsync(
            DeleteContactSprocName, 
            parameters, 
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Contact>> GetAllContactsAsync()
    {
        using var connection = new SqlConnection(ConnectionString);
        return await connection.QueryAsync<Contact>(
            GetAllContactsSprocName, 
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<Contact> GetFullContactAsync(int contactId)
    {
        using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("contactId", contactId);
        var models = await connection.QueryAsync<ContactDataModel>(
            GetFullContactDetailsSprocName, 
            parameters, 
            commandType: System.Data.CommandType.StoredProcedure);
        return GetContactFromDataModels(models);
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        using var connection = new SqlConnection(ConnectionString);
        var parameters = new DynamicParameters();
        parameters.Add("id", contact.Id);
        parameters.Add("firstName", contact.FirstName);
        parameters.Add("lastName", contact.LastName);
        parameters.Add("agency", contact.Agency);
        await connection.ExecuteAsync(
            UpdateContactSprocName, 
            parameters, 
            commandType: System.Data.CommandType.StoredProcedure);
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
