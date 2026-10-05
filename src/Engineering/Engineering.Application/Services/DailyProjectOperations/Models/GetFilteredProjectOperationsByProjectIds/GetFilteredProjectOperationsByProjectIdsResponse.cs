namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredProjectOperationsByProjectIds;

public record GetFilteredProjectOperationsByProjectIdsResponse(
    List<GetProjectOperationsByProjectIdsModel> Data, int RowCount);
