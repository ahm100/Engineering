using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractHeaderByIds;

public record GetsContractorContractHeaderByIdsQuery(
    List<long> Ids
    ) : IQuery<DataResult<List<ContractorContractHeader>>>;
