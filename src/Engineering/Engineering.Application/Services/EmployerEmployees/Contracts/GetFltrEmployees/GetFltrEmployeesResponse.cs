namespace Engineering.Application.Services.EmployerEmployees.Contracts.GetFltrEmployees;

public record GetFltrEmployeesResponse(
    List<GetFltrEmployeesModel> Data,
    int Count);

public class GetFltrEmployeesModel
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public string? Employee { get; set; }
    public long EmployerId { get; set; }
    public string? Employer { get; set; }
}