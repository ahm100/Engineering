
namespace Engineering.Application.Services.RequestContractors.Models.RequestContractorGroupDelete;

public record RequestContractorGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
