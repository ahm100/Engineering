using Engineering.Application.Services.EmployerEmployees.Contracts.GetEmployeesByEmployerId;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetFltrEmployees;
using Engineering.Domain.Entities.EmployerEmployees;

namespace Engineering.Application.Abstractions.Data.EmployerEmployees;

public interface IEmployerEmployeeRepository : IBaseRepository<EmployerEmployee>
{
    Task<EmployerEmployee?> GetById(
        long id, CT ct);

    Task<(List<GetEmployeesByEmployerIdModel>? Data, int RowCount)> GetEmployeesByEmployerId(
        long employerId,
        int pageIndex,
        int pageSize, CT ct);

    Task<List<EmployerEmployee>> GetThirdPartiesByEmployerId(
        List<long> employerIds, CT ct);

    Task<(List<GetFltrEmployeesModel>? Data, int RowCount)> GetFltrEmployees(
        int pageIndex,
        int pageSize, CT ct);
}