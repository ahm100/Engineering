namespace Engineering.Application.Services.OperationInfoGroupRelations.Models.GetsByOperationInfoId;

public record GetsOperationInfoGroupRelationByIdRequest(
    long OprationInfoId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
