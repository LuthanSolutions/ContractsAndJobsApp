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
                contacts.Add(GetObjectFromReader<Contact>(reader));
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
                models.Add(GetObjectFromReader<ContactDataModel>(reader));
            }
            return GetContactFromDataModels(models);
        }

        private static T GetObjectFromReader<T>(IDataReader reader)
        {
            var contactDataModel = (T)Activator.CreateInstance(typeof(T))!;
            foreach (var property in contactDataModel.GetType().GetProperties())
            {
                if (!DataReaderHasColumn(reader, property.Name)) continue;
                if (reader.IsDBNull(reader.GetOrdinal(property.Name))) continue;
                property.SetValue(contactDataModel, reader[property.Name]);
            }
            return contactDataModel;
        }

        private static bool DataReaderHasColumn(IDataReader reader, string columnName)
        {
            var schemaTable = reader.GetSchemaTable()!;
            var rows = schemaTable.Rows.OfType<DataRow>();
            return rows.Any(row => row["ColumnName"].ToString() == columnName);
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
