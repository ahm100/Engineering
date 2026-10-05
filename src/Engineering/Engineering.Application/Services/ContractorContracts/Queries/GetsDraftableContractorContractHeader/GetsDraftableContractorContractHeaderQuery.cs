using Engineering.Application.Services.ContractorContracts.Contracts.GetsDraftableContractorContractHeader;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsDraftableContractorContractHeader;

public record GetsDraftableContractorContractHeaderQuery(
    long ContractorId,
    long ProjectId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsDraftableContractorContractHeaderModel>>>;
