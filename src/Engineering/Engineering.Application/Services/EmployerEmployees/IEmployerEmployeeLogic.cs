using Engineering.Application.Services.EmployerEmployees.Contracts.CreateEmployerEmployee;
using Engineering.Application.Services.EmployerEmployees.Contracts.DeleteEmployerEmployee;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetEmployeesByEmployerId;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetFltrEmployees;
using Engineering.Application.Services.EmployerEmployees.Contracts.UpdateEmployerEmployee;

namespace Engineering.Application.Services.EmployerEmployees;

public interface IEmployerEmployeeLogic
{
    Task<Result<CreateEmployerEmployeeResponse?>> CreateEmployerEmployee(
        CreateEmployerEmployeeRequest request, CT ct);

    Task<Result<UpdateEmployerEmployeeResponse?>> UpdateEmployerEmployee(
        UpdateEmployerEmployeeRequest request, CT ct);

    Task<Result<DeleteEmployerEmployeeResponse?>> DeleteEmployerEmployee(
        DeleteEmployerEmployeeRequest request, CT ct);

    Task<Result<GetEmployeesByEmployerIdResponse?>> GetEmployeesByEmployerId(
        GetEmployeesByEmployerIdRequest request, CT ct);

    Task<Result<GetFltrEmployeesResponse?>> GetFltrEmployees(
        GetFltrEmployeesRequest request, CT ct);
}