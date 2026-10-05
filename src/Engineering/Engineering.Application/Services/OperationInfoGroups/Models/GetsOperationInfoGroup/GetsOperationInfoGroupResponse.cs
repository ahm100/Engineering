using Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupModels;

namespace Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroup;

public record GetsOperationInfoGroupResponse(
    List<GetsOperationInfoGroupModel> Data,
    int RowCount
    );
