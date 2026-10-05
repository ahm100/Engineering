using Engineering.Application.Services.EmployerEmployees.Contracts.GetEmployeesByEmployerId;

namespace Engineering.Application.Services.EmployerEmployees.Queries.GetEmployeesByEmployerId;

public record GetEmployeesByEmployerIdQuery(
    long EmployerId,
    int PageIndex,
    int PageSize) : IQuery<GetEmployeesByEmployerIdResponse?>;