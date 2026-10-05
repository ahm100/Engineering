using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Commands.ContractorEmployees.CreateContractorEmployee;

public record CreateContractorEmployeeCommand(long EmployeeId, long ContractorId, bool IsConfirm, bool IsActive, long? CompanyId) : ICommand<ContractorEmployee>;