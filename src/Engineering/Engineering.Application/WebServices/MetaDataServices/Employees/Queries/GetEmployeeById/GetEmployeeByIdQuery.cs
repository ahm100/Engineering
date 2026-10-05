using EmployeeModel = Engineering.Application.WebServices.MetaDataServices.Employees.Models.Employee;

namespace Engineering.Application.WebServices.MetaDataServices.Employees.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(
    long Id
    ) : IQuery<EmployeeModel?>;
