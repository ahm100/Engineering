
namespace Engineering.Application.Services.Seasons.Models.SeasonGroupDelete;

public record SeasonGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
