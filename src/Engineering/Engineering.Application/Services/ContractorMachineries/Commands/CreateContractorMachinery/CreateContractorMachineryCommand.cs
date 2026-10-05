using Engineering.Domain.Entities.ContractorMachineries.Enums;
using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.ContractorMachineries.Commands.CreateContractorMachinery;

public record CreateContractorMachineryCommand(
    Machinery Machinery,
    long ContractorId,
    decimal MachineryPrice,
    long CurrencyId,
    ContractorMachineryUnit Unit,
    string? NumberPlates,
    string? MachineryIdentifier,
    bool IsActive,
    string? Description,
    long? CompanyId) : ICommand<ContractorMachinery?>;