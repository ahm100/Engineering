namespace Engineering.Application.Services.RequestContractors.Models.GetsFilteredRequestContractorContractor;

public record GetsFilteredRequestContractorContractorRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
