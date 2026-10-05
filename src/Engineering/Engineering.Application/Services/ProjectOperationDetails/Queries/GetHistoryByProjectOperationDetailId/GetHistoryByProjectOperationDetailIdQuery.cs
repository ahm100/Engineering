using ProjectOperationDetailHistory = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailHistory;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetHistoryByProjectOperationDetailId;

public record GetHistoryByProjectOperationDetailIdQuery(
    long Id,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetailHistory>>>;