
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GroupProjectOperationDetailStatusChanger;

public record GroupProjectOperationDetailStatusChangerResponse(
    bool IsDone,
    long ProjectOperationId,
    ProjectOperationStatus Status,
    string StatsusDescription
    );
