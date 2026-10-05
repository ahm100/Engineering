namespace Engineering.Application.Services.ContractorContracts.Contracts.GetCCThirdParties;

public record GetCCThirdPartiesRequest(
    long ProjectId,
    List<long>? ContractorIds,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;