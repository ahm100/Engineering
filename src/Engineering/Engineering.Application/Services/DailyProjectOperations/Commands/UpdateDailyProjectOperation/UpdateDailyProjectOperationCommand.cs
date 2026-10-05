using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.UpdateDailyProjectOperation;

public record UpdateDailyProjectOperationCommand(
    long DailyProjectOperationId,
    ProjectOperationDetailStatus Status,
    DateTime StartDate,
    DateTime EndDate,
    decimal Length,
    decimal Width,
    decimal Height,
    decimal Weight,
    decimal Number,
    string? Description,
    long? CompanyId
    ) : ICommand<DailyProjectOperation>;
