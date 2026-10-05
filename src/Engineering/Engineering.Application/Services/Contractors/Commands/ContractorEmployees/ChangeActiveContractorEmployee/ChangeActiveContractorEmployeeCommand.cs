using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Commands.ContractorEmployees.ChangeActiveContractorEmployee;

public record ChangeActiveContractorEmployeeCommand(long Id,
                                                    bool? IsActive,
                                                    bool IsConfirm) : ICommand<ContractorEmployee>;

