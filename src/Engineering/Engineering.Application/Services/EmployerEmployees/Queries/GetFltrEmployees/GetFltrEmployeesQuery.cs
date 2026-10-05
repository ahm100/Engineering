using Engineering.Application.Services.EmployerEmployees.Contracts.GetFltrEmployees;

namespace Engineering.Application.Services.EmployerEmployees.Queries.GetFltrEmployees;

public record GetFltrEmployeesQuery(
    int PageIndex,
    int PageSize) : IQuery<GetFltrEmployeesResponse?>;