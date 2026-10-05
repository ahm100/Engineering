namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetFilteredProjectOperationDetailInspections;

public record GetFilteredProjectOperationDetailInspectionsResponse(
    List<GetFilteredProjectOperationDetailInspectionsModel> Data,
    int RowCount);
