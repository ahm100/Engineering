namespace Engineering.Application.Services.OperationInfoGroupRelations.Models.OperationInfoGroupRelationModels;

public record GetsOperationInfoGroupRelationResponseModel(
    List<GroupRelationsOperationInfoModel>? OperationInfoData,
    List<GroupRelationsOperationInfoGroupModel>? OperationInfoGroupData
    );
