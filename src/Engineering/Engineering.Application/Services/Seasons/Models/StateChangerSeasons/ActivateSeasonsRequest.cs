
namespace Engineering.Application.Services.Seasons.Models.StateChangerSeasons;

public record ActivateSeasonsRequest(
    List<long> Ids
    ) : IHttpRequest;
