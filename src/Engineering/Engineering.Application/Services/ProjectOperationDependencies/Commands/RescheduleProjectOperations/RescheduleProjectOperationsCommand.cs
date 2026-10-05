namespace Engineering.Application.Services.ProjectOperationDependencies.Commands.RescheduleProjectOperations;

public record RescheduleProjectOperationsCommand(
    long ProjectOperationId
    ) : ICommand<bool>;