namespace Engineering.Application.Services.Seasons.Models.GetSeasonByName;

public record GetSeasonByNameRequest(
    string SeasonName,
    long BranchId
     ) : IHttpRequest;
