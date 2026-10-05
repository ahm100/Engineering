namespace Engineering.Application.Services.Seasons.Models.DisableSeason;

public record DisableSeasonRequest(
    long Id
     ) : IHttpRequest;
