namespace ContractsAndJobs.Data.DataModels;

public class ContactDataModel
{
    public int? ContactId { get; set; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Agency { get; set; }
    public int? InteractionId { get; set; }
    public int? InteractionContactId { get; set; }
    public int? InteractionRoleId { get; set; }
    public DateTime? Date { get; set; }
    public int? RoleId { get; set; }
    public string? Company { get; set; }
    public int? Type { get; set; }
    public int? WorkType { get; set; }
    public decimal? Salary { get; set; }
    public decimal? DayRate { get; set; }
    public bool? InsideIr35 { get; set; }
    public string? Location { get; set; }
}
