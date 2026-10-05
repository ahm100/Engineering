namespace Engineering.Application.Services.EmployerEmployees.Contracts.GetFltrEmployees;

public record GetFltrEmployeesRequest(
    int PageIndex,
    int PageSize
     ) : IHttpRequest;