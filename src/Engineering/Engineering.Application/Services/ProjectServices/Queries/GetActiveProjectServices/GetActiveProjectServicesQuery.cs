using Engineering.Application.Services.ProjectServices.Models.ProjectServiceModels;

namespace Engineering.Application.Services.ProjectServices.Queries.GetActiveProjectServices;

public record GetActiveProjectServicesQuery(
        List<long>? Ids,
        List<long>? CostcenterIds,
        List<long>? ProjectIds,
        List<long>? ServiceInfoIds,
        List<long>? ContractorIds,
        string? ServiceFilterData,
        string? FilterData,
        int PageIndex,
        int PageSize
    ) : IQuery<DataResult<List<GetsActiveProjectServiceModel>>>;