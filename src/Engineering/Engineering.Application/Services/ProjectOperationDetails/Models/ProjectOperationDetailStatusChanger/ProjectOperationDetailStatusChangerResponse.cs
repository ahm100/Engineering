
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailStatusChanger;

public record ProjectOperationDetailStatusChangerResponse(
    long Id,
    long ProjectOperationId,
    ProjectOperationStatus Status,
    string StatsusDescription
    );
