
namespace Engineering.Application.Services.ProjectOperations.Models.GetsSummaryProjectOperation;

public record GetsSummaryProjectOperationResponse(
    List<GetsSummaryProjectOperationModel> Data,
    int RowCount);
