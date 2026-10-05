namespace Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupByCode;

public record GetOperationInfoGroupByCodeRequest(
    string OperationInfoGroupCode
     ) : IHttpRequest;
