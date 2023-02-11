namespace ContractsAndJobs.Models;

public class Contact
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Agency { get; set; }
    public string FullName => $"{FirstName} {LastName} - {Agency}";
    public IEnumerable<Interaction>? Interactions { get; set; }
}