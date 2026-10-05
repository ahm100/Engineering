using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractHeader;

public record GetsContractorContractHeaderQuery(
    long ContractorId,
    long ProjectId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ContractorContractHeader>>>;
