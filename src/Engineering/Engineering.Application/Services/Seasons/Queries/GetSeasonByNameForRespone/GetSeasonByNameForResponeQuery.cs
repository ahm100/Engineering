using Engineering.Application.Services.Seasons.Models.GetSeasonByName;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByNameForRespone;

public record GetSeasonByNameForResponeQuery(
    string SeasonName,
    long BranchId) : IQuery<GetSeasonByNameResponse?>;