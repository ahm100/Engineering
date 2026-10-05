
namespace Engineering.Application.Services.RequestMachineries.Models.RequestMachineryGroupDelete;

public record RequestMachineryGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
