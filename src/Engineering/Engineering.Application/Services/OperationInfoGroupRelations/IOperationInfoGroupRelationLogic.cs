using Engineering.Application.Services.OperationInfoGroupRelations.Models.CreateOperationInfoGroupRelation;
using Engineering.Application.Services.OperationInfoGroupRelations.Models.GetsByOperationInfoGroupId;
using Engineering.Application.Services.OperationInfoGroupRelations.Models.GetsByOperationInfoId;

namespace Engineering.Application.Services.OperationInfoGroupRelations;

public interface IOperationInfoGroupRelationLogic
{
    Task<Result<CreateOperationInfoGroupRelationResponse?>> CreateOperationInfoGroupRelation(
        CreateOperationInfoGroupRelationRequest request, CT ct);

    Task<Result<GetsOperationInfoGroupRelationByIdResponse?>> GetsByOperationInfoId(
        GetsOperationInfoGroupRelationByIdRequest request, CT ct);

    Task<Result<GetsOperationInfoGroupRelationByGroupIdResponse?>> GetsByOperationInfoGroupId(
        GetsOperationInfoGroupRelationByGroupIdRequest request, CT ct);
}