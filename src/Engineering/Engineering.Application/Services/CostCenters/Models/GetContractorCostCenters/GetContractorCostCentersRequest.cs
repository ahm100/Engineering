namespace Engineering.Application.Services.CostCenters.Models.GetContractorCostCenters;

public record GetContractorCostCentersRequest(
    long ContractorId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
