
namespace Engineering.Application.Services.Seasons.Models.StateChangerSeasons;

public record StateChangerSeasonsRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
