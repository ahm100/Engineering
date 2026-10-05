
namespace Engineering.Application.Services.OperationInfoGroups.Models.StateChangerOperationInfoGroups;

public record StateChangerOperationInfoGroupsRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
