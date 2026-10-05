using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UpdateProjectOperationPrice;

public record UpdateProjectOperationPriceCommand(
    ProjectOperation ProjectOperation,
    decimal? Price
    ) : ICommand<ProjectOperation>;