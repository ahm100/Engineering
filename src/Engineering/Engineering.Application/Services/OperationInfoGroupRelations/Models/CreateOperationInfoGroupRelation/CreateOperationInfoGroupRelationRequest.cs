namespace Engineering.Application.Services.OperationInfoGroupRelations.Models.CreateOperationInfoGroupRelation;

public record CreateOperationInfoGroupRelationRequest(
    long OperationInfoId,
    List<long>? OperationInfoGroupIds
     ) : IHttpRequest;
