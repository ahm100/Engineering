namespace Engineering.Application.Services.EmployerEmployees.Contracts.UpdateEmployerEmployee;

public record UpdateEmployerEmployeeRequest(
    long Id,
    long? EmployerId,
    bool? IsActive) : IHttpRequest;