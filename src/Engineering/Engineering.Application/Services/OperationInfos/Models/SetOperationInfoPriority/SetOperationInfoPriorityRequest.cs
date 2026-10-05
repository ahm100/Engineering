namespace Engineering.Application.Services.OperationInfos.Models.SetOperationInfoPriority;

public record SetOperationInfoPriorityRequest(
    long Id,
    int? SetPriority
     ) : IHttpRequest;
