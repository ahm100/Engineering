namespace Engineering.Application.Services.EmployerEmployees.Contracts.DeleteEmployerEmployee;

public record DeleteEmployerEmployeeRequest(
    long Id) : IHttpRequest;