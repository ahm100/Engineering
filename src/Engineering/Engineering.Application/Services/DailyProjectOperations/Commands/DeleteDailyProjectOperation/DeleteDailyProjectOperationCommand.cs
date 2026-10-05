using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.DeleteDailyProjectOperation;

public record DeleteDailyProjectOperationCommand(
    long Id
    ) : ICommand<DailyProjectOperation>;
