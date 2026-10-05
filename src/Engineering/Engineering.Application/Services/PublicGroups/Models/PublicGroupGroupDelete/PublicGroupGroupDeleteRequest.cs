
namespace Engineering.Application.Services.PublicGroups.Models.PublicGroupGroupDelete;

public record PublicGroupGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
