namespace Engineering.Application.Services.EmployerEmployees.Contracts.GetEmployeesByEmployerId;

public record GetEmployeesByEmployerIdResponse(
    List<GetEmployeesByEmployerIdModel> Data,
    int Count);
public class GetEmployeesByEmployerIdModel
{
    public long Id { get; set; }
    public long EmployeeId { get; set; }
    public string? Employee { get; set; }
    public long EmployerId { get; set; }
    public string? Employer { get; set; }
}
