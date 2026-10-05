namespace Engineering.Application.Services.OperationInfoGroupRelations.Models.GetsByOperationInfoGroupId;

public record GetsOperationInfoGroupRelationByGroupIdRequest(
    long OprationInfoGroupId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
