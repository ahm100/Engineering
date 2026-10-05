
namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationByIds;

public record GetsProjectOperationByIdsResponse(
    List<GetsProjectOperationByIdsResponseModel> Data,
    int RowCount);
