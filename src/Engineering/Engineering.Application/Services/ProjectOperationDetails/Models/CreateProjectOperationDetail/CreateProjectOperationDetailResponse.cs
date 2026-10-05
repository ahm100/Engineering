using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.CreateProjectOperationDetail;

public record CreateProjectOperationDetailResponse(
    long Id,
    decimal Workload,
    long ProjectOperationId,
    ProjectOperationStatus Status,
    string StatsusDescription
    );
