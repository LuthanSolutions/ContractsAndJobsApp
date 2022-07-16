namespace ContractsAndJobs.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Agency { get; set; }
        public string? FullName => $"{this.FirstName} {this.LastName} - {this.Agency}";
        public IEnumerable<Interaction>? Interactions { get; set; }
    }
}