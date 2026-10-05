namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetCurrentUserTemporaryDailies;

public record GetCurrentUserTemporaryDailiesResponse(
    List<GetCurrentUserTemporaryDailiesModel> Data,
    int RowCount);
