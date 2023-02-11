namespace ContractsAndJobs.Models;

public class Role
{
    public int Id { get; set; }
    public string? Company { get; set; }
    public RoleTypes Type { get; set; }
    public WorkTypes WorkType { get; set; }
    public decimal Salary { get; set; }
    public decimal DayRate { get; set; }
    public bool InsideIr35 { get; set; }
    public string? Location { get; set; }
}