
namespace Engineering.Application.Services.ProjectOperationDetails.Models.DeleteProjectOperationDetail;

public record DeleteProjectOperationDetailResponse(
    long Id,
    bool IsDeleted,
    decimal Workload
    );
