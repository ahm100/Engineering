
namespace Engineering.Application.Services.OperationInfoGroups.Models.StateChangerOperationInfoGroups;

public record ActivateOperationInfoGroupsRequest(
    List<long> Ids
    ) : IHttpRequest;
