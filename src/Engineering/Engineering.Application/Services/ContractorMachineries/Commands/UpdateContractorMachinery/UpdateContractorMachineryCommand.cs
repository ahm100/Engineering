using Engineering.Domain.Entities.ContractorMachineries.Enums;
using Engineering.Domain.Entities.Machineries;
using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Commands.UpdateContractorMachinery;

public record UpdateContractorMachineryCommand(
    long Id,
    Machinery Machinery,
    long ContractorId,
    decimal MachineryPrice,
    long CurrencyId,
    ContractorMachineryUnit Unit,
    string? NumberPlates,
    string? MachineryIdentifier,
    bool IsActive,
    string? Description,
    long? CompanyId
    ) : ICommand<ContractorMachinery>;