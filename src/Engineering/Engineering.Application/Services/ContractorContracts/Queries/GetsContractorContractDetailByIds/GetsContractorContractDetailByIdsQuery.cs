using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractDetailByIds;

public record GetsContractorContractDetailByIdsQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<ContractorContractDetail>>>;
