
namespace Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupGroupDelete;

public record OperationInfoGroupGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
