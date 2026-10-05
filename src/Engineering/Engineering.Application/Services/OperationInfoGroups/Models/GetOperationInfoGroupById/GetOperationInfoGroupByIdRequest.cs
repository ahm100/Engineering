namespace Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupById;

public record GetOperationInfoGroupByIdRequest(
    long Id
     ) : IHttpRequest;
