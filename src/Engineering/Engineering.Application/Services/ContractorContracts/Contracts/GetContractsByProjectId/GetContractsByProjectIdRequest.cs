namespace Engineering.Application.Services.ContractorContracts.Contracts.GetContractsByProjectId;

public record GetContractsByProjectIdRequest(
    long ProjectId,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;