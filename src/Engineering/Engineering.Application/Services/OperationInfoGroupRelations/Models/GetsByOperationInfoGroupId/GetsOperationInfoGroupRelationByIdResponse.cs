
using Engineering.Application.Services.OperationInfoGroupRelations.Models.OperationInfoGroupRelationModels;

namespace Engineering.Application.Services.OperationInfoGroupRelations.Models.GetsByOperationInfoGroupId;

public record GetsOperationInfoGroupRelationByGroupIdResponse(
    List<GroupRelationsOperationInfoModel>? OperationInfoData,
    List<GroupRelationsOperationInfoGroupModel>? OperationInfoGroupData
    );
