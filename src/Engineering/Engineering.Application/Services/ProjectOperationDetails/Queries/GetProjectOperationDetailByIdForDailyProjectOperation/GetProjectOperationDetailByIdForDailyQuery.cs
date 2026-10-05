using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdForDailyProjectOperation;

public record GetProjectOperationDetailByIdForDailyQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;