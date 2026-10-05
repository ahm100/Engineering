
namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoGroupDelete;

public record OperationInfoGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
