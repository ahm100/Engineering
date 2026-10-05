using Engineering.Application.Services.ProjectServices.Models.ProjectServiceModels;

namespace Engineering.Application.Services.ProjectServices.Queries.GetProjectServices;

public record GetProjectServicesQuery(
        long ProjectId,
        string? FilterData,
        int PageIndex,
        int PageSize
    ) : IQuery<DataResult<List<GetsContractorServiceModel>>>;