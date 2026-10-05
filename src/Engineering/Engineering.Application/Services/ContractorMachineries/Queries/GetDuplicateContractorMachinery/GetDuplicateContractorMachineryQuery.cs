using Engineering.Domain.Entities.ContractorMachineries;
using Engineering.Domain.Entities.ContractorMachineries.Enums;

namespace Engineering.Application.Services.ContractorMachineries.Queries.GetDuplicateContractorMachinery;

public record GetDuplicateContractorMachineryQuery(
    long ContractorId,
    long MachineryId,
    ContractorMachineryUnit Unit,
    long? CompanyId
    ) : IQuery<ContractorMachinery?>;