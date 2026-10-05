namespace Engineering.Application.Services.ProjectServices.Models.GetsProjectServiceDetail;

public record GetsProjectServiceDetailRequest(
        List<long>? Ids,
        List<long>? CostcenterIds,
        List<long>? ProjectIds,
        List<long>? ServiceInfoIds,
        List<long>? ContractorIds,
        List<long>? ProjectOperationIds,
        bool? IsActive,
        string? ServiceFilterData,
        string? FilterData,
        int PageIndex,
        int PageSize
     ) : IHttpRequest;
