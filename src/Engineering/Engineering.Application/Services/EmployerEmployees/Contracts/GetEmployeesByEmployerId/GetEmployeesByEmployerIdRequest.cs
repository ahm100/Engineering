namespace Engineering.Application.Services.EmployerEmployees.Contracts.GetEmployeesByEmployerId;

public record GetEmployeesByEmployerIdRequest(
    long EmployerId,
    int PageIndex,
    int PageSize) : IHttpRequest;