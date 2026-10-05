using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperation;

public record UpdateDailyProjectOperationResponse(long Id,
    long ProjectOperationId,
    ProjectOperationStatus Status,
    string StatsusDescription);
