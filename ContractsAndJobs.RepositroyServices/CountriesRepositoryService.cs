using ContractsAndJobs.Models;
using DataServices.Services;
using System.Data;
using System.Data.SqlClient;

namespace ContractsAndJobs.RepositroyServices;

public interface ICountriesRepositoryService
{
    Task<IEnumerable<Country>> GetAllCountriesAsync();
}

public class CountriesRepositoryService : ICountriesRepositoryService
{
    private readonly IDataService dataService;

    public CountriesRepositoryService(IDataService dataService)
    {
        this.dataService = dataService;
    }

    private SqlConnection? connection;
    private const string ConnectionString = "data source=LUTHANSOLUTIONS;initial catalog=CountriesAndRegions;trusted_connection=true";

    public async Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        var countries = new List<Country>();
        connection = new SqlConnection(ConnectionString);
        await using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "SelectCountries";
        await connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            countries.Add(dataService.GetObjectFromReader<Country>(reader));
        }
        return countries;
    }
}
