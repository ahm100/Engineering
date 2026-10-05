namespace Engineering.Application.Services.Seasons.Models.ActiveSeason;

public record ActiveSeasonRequest(
    long Id
     ) : IHttpRequest;
