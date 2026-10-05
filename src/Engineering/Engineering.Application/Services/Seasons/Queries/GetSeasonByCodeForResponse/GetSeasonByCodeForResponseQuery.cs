using Engineering.Application.Services.Seasons.Models.GetSeasonByCode;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByCodeForResponse;

public record GetSeasonByCodeForResponseQuery(
    string SeasonCode,
    long BranchId) : IQuery<GetSeasonByCodeResponse?>;