using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Commands.ContractorEmployees.UpdateContractorEmployee;

public record UpdateContractorEmployeeCommand(long Id,
                                              long EmployeeId,
                                              long ContractorId,
                                              long? CompanyId) : ICommand<ContractorEmployee>;