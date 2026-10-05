namespace Engineering.Application.Services.ContractorContracts.Contracts.GetFltrProjectContractors;

public record GetFltrProjectContractorsRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;