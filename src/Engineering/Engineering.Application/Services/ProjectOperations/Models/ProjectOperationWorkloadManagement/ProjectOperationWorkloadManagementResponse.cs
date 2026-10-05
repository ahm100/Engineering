
namespace Engineering.Application.Services.ProjectOperations.Models.ProjectOperationWorkloadManagement;

public record ProjectOperationWorkloadManagementResponse(
    long Id,
    decimal? Workload,
    decimal? UsedWorkload,
    decimal? RemainingWorkload
    );
