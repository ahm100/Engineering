namespace Engineering.Application.Services.Seasons.Models.InactiveSeason;

public record InactiveSeasonRequest(
    long Id
     ) : IHttpRequest;