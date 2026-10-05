namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoByName;

public record GetOperationInfoByNameRequest(
    string OperationInfoName
     ) : IHttpRequest;
