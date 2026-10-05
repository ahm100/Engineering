namespace Engineering.Application.Services.ProjectOperations.Command.SetPOActualDate;

public record SetPOActualDateCommand(
    long ProjectOperationId,
    DateTime DailyProjectOperationDate
    ) : ICommand<bool?>;