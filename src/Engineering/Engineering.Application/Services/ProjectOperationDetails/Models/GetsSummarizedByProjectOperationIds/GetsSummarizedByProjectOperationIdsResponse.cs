
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsSummarizedByProjectOperationIds;

public record GetsSummarizedByProjectOperationIdsResponse(
    List<GetsSummarizedByProjectOperationIdsModel> Data,
    int RowCount
    );
