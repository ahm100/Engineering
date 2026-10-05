namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoByCode;

public record GetOperationInfoByCodeRequest(
    string OperationInfoCode
     ) : IHttpRequest;
