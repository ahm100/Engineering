using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.CreateDailyProjectOperation;

public record CreateDailyProjectOperationResponse(long Id,
    long ProjectOperationId,
    ProjectOperationStatus Status,
    string StatsusDescription);