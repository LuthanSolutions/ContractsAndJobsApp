using System.Data;
using ContractsAndJobs.Models;
using System.Data.SqlClient;
using ContractsAndJobs.Data.DataModels;

namespace ContractsAndJobs.Data
{
    public interface IContractsAndJobsDataService
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<Contact> GetFullContact(int contactId);
    }

    public class ContractsAndJobsDataService : IContractsAndJobsDataService
    {
        private SqlConnection? connection;
        private const string ConnectionString = "data source=LUTHANSOLUTIONS;initial catalog=ContractsAndJobs;trusted_connection=true";
        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            var contacts = new List<Contact>();
            this.connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetAllContacts";
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                contacts.Add(new Contact
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    FirstName = !await reader.IsDBNullAsync("FirstName") ? reader.GetString(reader.GetOrdinal("FirstName")) : null,
                    LastName = !await reader.IsDBNullAsync("LastName") ? reader.GetString(reader.GetOrdinal("LastName")) : null,
                    Agency = !await reader.IsDBNullAsync("Agency") ? reader.GetString(reader.GetOrdinal("Agency")) : null,
                });
            }
            return contacts;
        }

        public async Task<Contact> GetFullContact(int contactId)
        {
            var models = new List<ContactDataModel>();
            this.connection = new SqlConnection(ConnectionString);
            await using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetFullContactDetails";
            command.Parameters.AddWithValue("@contactId", contactId);
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                models.Add(new ContactDataModel
                {
                        ContactId = await reader.IsDBNullAsync("ContactId") ? null : reader.GetInt32(reader.GetOrdinal("ContactId")),
                        FirstName = await reader.IsDBNullAsync("FirstName") ? null : reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = await reader.IsDBNullAsync("LastName") ? null : reader.GetString(reader.GetOrdinal("LastName")),
                        Agency = await reader.IsDBNullAsync("Agency") ? null : reader.GetString(reader.GetOrdinal("Agency")),
                        InteractionId = await reader.IsDBNullAsync("InteractionId") ? null : reader.GetInt32(reader.GetOrdinal("InteractionId")),
                        InteractionContactId = await reader.IsDBNullAsync("InteractionContactId") ? null : reader.GetInt32(reader.GetOrdinal("InteractionContactId")),
                        Date = await reader.IsDBNullAsync("Date") ? null : reader.GetDateTime(reader.GetOrdinal("Date")),
                        InteractionRoleId = await reader.IsDBNullAsync("InteractionRoleId") ? null : reader.GetInt32(reader.GetOrdinal("InteractionRoleId")),
                        RoleId = await reader.IsDBNullAsync("RoleId") ? null : reader.GetInt32(reader.GetOrdinal("RoleId")),
                        Company = await reader.IsDBNullAsync("Company") ? null : reader.GetString(reader.GetOrdinal("Company")),
                        Type = await reader.IsDBNullAsync("Type") ? null : reader.GetInt32(reader.GetOrdinal("Type")),
                        WorkType = await reader.IsDBNullAsync("WorkType") ? null : reader.GetInt32(reader.GetOrdinal("WorkType")),
                        Salary = await reader.IsDBNullAsync("Salary") ? null : reader.GetDecimal(reader.GetOrdinal("Salary")),
                        DayRate = await reader.IsDBNullAsync("DayRate") ? null : reader.GetDecimal(reader.GetOrdinal("DayRate")),
                        InsideIr35 = await reader.IsDBNullAsync("InsideIr35") ? null : reader.GetBoolean(reader.GetOrdinal("InsideIr35")),
                        Location = await reader.IsDBNullAsync("Location") ? null : reader.GetString(reader.GetOrdinal("Location"))
                    });
            }
            return GetContactFromDataModels(models);
        }

        private static Contact GetContactFromDataModels(IList<ContactDataModel> dataModels)
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
                        .Select(i => new Interaction()
                        {
                            Id = i.Key!.Value,
                            Date = i.First().Date!.Value,
                            Role = new Role
                            {
                                WorkType = (WorkTypes)i.First().WorkType!.Value,
                                Type = (RoleTypes)i.First().Type!.Value,
                                Id = i.First().RoleId!.Value,
                                Company = i.First().Company,
                                DayRate = i.First().DayRate!.HasValue ? i.First().DayRate!.Value : 0,
                                Salary = i.First().Salary!.HasValue ? i.First().Salary!.Value : 0,
                                InsideIr35 = i.First().InsideIr35!.HasValue && i.First().InsideIr35!.Value,
                                Location = i.First().Location
                            }
                        })
                }).ToList().First();
        }
    }
}
