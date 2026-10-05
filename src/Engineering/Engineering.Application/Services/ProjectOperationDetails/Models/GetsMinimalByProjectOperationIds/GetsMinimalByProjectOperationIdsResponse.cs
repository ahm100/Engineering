
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsMinimalByProjectOperationIds;

public record GetsMinimalByProjectOperationIdsResponse(
    List<GetsMinimalByProjectOperationIdsModel> Data,
    int RowCount
    );
