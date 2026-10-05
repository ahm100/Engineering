using Engineering.Application.Services.ProjectOperations.Models.Models;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationByProjectIds;

public record GetsProjectOperationByProjectIdsResponse(
    List<GetsProjectOperationByProjectModel> Data,
    int RowCount);
