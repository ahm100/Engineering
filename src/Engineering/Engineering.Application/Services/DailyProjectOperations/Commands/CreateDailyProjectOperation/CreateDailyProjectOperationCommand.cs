using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperation;

public record CreateDailyProjectOperationCommand(
    ProjectOperationDetailStatus Status,
    DateTime StartDate,
    DateTime EndDate,
    decimal Length,
    decimal Width,
    decimal Height,
    decimal Weight,
    decimal Number,
    ProjectOperationDetail ProjectOperationDetail,
    string? Description,
    long? LegacyId,
    long? CompanyId
    ) : ICommand<DailyProjectOperation>;
