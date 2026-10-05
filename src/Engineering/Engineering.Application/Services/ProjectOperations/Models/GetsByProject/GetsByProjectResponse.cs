using Engineering.Application.Services.ProjectOperations.Models.Models;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsByProject;

public record GetsByProjectResponse(
    List<GetsProjectOperationByProjectModel> Data,
    int RowCount);
