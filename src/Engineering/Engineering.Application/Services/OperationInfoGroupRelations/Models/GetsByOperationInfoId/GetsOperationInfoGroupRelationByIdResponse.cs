
using Engineering.Application.Services.OperationInfoGroupRelations.Models.OperationInfoGroupRelationModels;

namespace Engineering.Application.Services.OperationInfoGroupRelations.Models.GetsByOperationInfoId;

public record GetsOperationInfoGroupRelationByIdResponse(
    List<GroupRelationsOperationInfoModel>? OperationInfoData,
    List<GroupRelationsOperationInfoGroupModel>? OperationInfoGroupData
    );
