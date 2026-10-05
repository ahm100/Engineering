namespace Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupByName;

public record GetOperationInfoGroupByNameRequest(
    string OperationInfoGroupName
     ) : IHttpRequest;
