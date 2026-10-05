
namespace Engineering.Application.Services.ProjectOperations.Models.GetsSummarizedProjectOperation;

public record GetsSummarizedProjectOperationResponse(
    List<GetsSummarizedProjectOperationModel> Data,
    int RowCount);
