namespace ContractsAndJobs.Models
{
    public class Interaction
    {
        public int Id { get; set; }
        public Contact? Contact { get; set; }
        public DateTime Date { get; set; }
        public Role? Role { get; set; }
    }
}