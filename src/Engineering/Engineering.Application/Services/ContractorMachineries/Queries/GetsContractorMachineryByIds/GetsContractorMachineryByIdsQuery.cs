using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Queries.GetsContractorMachineryByIds;

public record GetsContractorMachineryByIdsQuery(
    List<long> ids,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ContractorMachinery?>>>;