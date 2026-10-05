using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Commands.ContractorEmployees.DeleteContractorEmployee;

public record DeleteContractorEmployeeCommand(long Id) : ICommand<ContractorEmployee>;

