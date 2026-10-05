using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Queries.GetContractorMachineryById;

public record GetContractorMachineryByIdQuery(
    long Id
    ) : IQuery<ContractorMachinery?>;