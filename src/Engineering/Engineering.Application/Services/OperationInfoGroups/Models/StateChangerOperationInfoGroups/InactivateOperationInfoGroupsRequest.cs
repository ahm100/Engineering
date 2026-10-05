
namespace Engineering.Application.Services.OperationInfoGroups.Models.StateChangerOperationInfoGroups;

public record InactivateOperationInfoGroupsRequest(
    List<long> Ids
    ) : IHttpRequest;
