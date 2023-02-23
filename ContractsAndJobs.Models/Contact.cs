namespace ContractsAndJobs.Models;

public class Contact
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Agency { get; set; }
    public string FullName => $"{this.FirstName} {this.LastName} - {this.Agency}";
    public IEnumerable<Interaction>? Interactions { get; set; }
}