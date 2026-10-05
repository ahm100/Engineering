using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperations.Queries.FindMergedProjectOperation;

public record FindMergedProjectOperationQuery(
    long ProjectOperationId,
    long ProjectId,
    long UnitOfMeasurementId,
    long OperationInfoId
    ) : IQuery<ProjectOperation>;