using Engineering.Application.Services.ProjectOperations.Models.Models;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsFilteredByProject;

public record GetsFilteredByProjectResponse(
    List<GetsProjectOperationByProjectModel> Data,
    int RowCount);
