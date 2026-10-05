
namespace Engineering.Application.Services.ServiceInfos.Models.ServiceInfoGroupDelete;

public record ServiceInfoGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
