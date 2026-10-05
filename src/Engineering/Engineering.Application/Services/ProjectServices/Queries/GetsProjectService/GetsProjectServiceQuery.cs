using Engineering.Application.Services.ProjectServices.Models.GetsProjectService;

namespace Engineering.Application.Services.ProjectServices.Queries.GetsProjectService;

public record GetsProjectServiceQuery(
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
    ) : IQuery<DataResult<List<GetsProjectServiceModel>>>;