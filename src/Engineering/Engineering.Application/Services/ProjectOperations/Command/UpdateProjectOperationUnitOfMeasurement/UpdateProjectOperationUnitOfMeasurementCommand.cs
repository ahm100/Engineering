using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationUnitOfMeasurement;

public record UpdateProjectOperationUnitOfMeasurementCommand(
    List<ProjectOperation> ProjectOperations,
    long UnitOfMeasurementId
    ) : ICommand<List<ProjectOperation>>;