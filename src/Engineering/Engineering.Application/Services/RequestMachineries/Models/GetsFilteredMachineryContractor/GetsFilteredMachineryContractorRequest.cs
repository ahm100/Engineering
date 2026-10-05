namespace Engineering.Application.Services.RequestMachineries.Models.GetsFilteredMachineryContractor;

public record GetsFilteredMachineryContractorRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
