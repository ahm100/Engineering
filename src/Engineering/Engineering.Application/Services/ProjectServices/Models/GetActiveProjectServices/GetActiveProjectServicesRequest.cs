namespace Engineering.Application.Services.ProjectServices.Models.GetActiveProjectServices;

public record GetActiveProjectServicesRequest(
        List<long>? Ids,
        List<long>? CostcenterIds,
        List<long>? ProjectIds,
        List<long>? ServiceInfoIds,
        List<long>? ContractorIds,
        string? ServiceFilterData,
        string? FilterData,
        int PageIndex,
        int PageSize
     ) : IHttpRequest;