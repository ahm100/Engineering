
namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationForValidating;

public record GetProjectOperationForValidatingQuery(
    long OprationInfoId,
    long ProjectId,
    long UnitOfMeasurementId
    ) : IQuery<bool>;