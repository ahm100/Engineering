using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Queries.GetProjectOperationSchedulings;

public record GetProjectOperationSchedulingsQuery(
    long ProjectId,
    long OperationInfoId
    ) : IQuery<List<ProjectOperation>>;
