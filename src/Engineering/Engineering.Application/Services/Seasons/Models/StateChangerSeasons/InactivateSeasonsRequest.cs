
namespace Engineering.Application.Services.Seasons.Models.StateChangerSeasons;

public record InactivateSeasonsRequest(
    List<long> Ids
    ) : IHttpRequest;
