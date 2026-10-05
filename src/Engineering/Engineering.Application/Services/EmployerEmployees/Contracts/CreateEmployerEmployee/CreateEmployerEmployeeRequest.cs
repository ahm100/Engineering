namespace Engineering.Application.Services.EmployerEmployees.Contracts.CreateEmployerEmployee;

public record CreateEmployerEmployeeRequest(
    long EmployeeId,
    long EmployerId,
    bool IsActive) : IHttpRequest;