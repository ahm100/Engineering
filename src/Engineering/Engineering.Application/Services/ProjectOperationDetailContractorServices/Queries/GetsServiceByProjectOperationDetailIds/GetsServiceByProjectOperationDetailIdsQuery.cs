using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsServiceByProjectOperationDetailIds;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsServiceByProjectOperationDetailIds;

public record GetsServiceByProjectOperationDetailIdsQuery(
    List<long> ProjectOperationDetailIds,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsServiceByProjectOperationDetailIdsModel>>>;