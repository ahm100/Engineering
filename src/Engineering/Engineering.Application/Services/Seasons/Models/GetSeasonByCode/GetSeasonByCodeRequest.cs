namespace Engineering.Application.Services.Seasons.Models.GetSeasonByCode;

public record GetSeasonByCodeRequest(
    string SeasonCode,
    long BranchId
     ) : IHttpRequest;
