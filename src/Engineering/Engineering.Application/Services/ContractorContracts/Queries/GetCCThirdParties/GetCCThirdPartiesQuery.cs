using Engineering.Application.Services.ContractorContracts.Contracts.GetCCThirdParties;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetCCThirdParties;

public record GetCCThirdPartiesQuery(
    long ProjectId,
    List<long>? ContractorIds,
    int PageIndex,
    int PageSize,
    long CompanyId
    ) : IQuery<GetCCThirdPartiesResponse?>;
