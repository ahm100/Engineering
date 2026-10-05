
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByIds;

public record GetsProjectOperationDetailByIdsResponse(
    List<GetsProjectOperationDetailByIdsModel> Data,
    int RowCount
    );
