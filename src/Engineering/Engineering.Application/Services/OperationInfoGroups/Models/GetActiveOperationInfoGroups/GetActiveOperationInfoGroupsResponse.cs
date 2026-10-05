using Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupModels;

namespace Engineering.Application.Services.OperationInfoGroups.Models.GetActiveOperationInfoGroups;

public record GetActiveOperationInfoGroupsResponse(
    List<GetsActiveOperationInfoGroupModel> Data,
    int RowCount);
