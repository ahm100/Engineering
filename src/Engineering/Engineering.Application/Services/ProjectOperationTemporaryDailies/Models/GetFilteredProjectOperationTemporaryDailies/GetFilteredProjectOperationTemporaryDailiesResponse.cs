namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetFilteredProjectOperationTemporaryDailies;

public record GetFilteredProjectOperationTemporaryDailiesResponse(
    List<GetFilteredProjectOperationTemporaryDailiesModel> Data,
    int RowCount);
