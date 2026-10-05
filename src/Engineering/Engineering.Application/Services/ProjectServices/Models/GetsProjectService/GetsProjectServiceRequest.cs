namespace Engineering.Application.Services.ProjectServices.Models.GetsProjectService;

public record GetsProjectServiceRequest(
        List<long>? Ids,
        List<long>? CostcenterIds,
        List<long>? ProjectIds,
        List<long>? ServiceInfoIds,
        List<long>? ContractorIds,
        bool? IsActive,
        string? ServiceFilterData,
        string? FilterData,
        int PageIndex,
        int PageSize
     ) : IHttpRequest;
