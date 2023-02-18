namespace ContractsAndJobs.Models;

public class Country
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? FlagSvg { get; set; }
}
