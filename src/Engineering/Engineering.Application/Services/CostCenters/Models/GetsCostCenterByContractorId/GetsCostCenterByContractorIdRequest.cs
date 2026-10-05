namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterByContractorId;

public record GetsCostCenterByContractorIdRequest(
    string? FilterData,
    long ContractorId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
