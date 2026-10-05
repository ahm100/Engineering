using Engineering.Application.Services.Seasons.Models.GetActiveSeasons;
using Engineering.Application.Services.Seasons.Models.SeasonModels;

namespace Engineering.Application.Services.Seasons.Queries.GetActiveSeasonsForResponse;

public record GetActiveSeasonsForResponseQuery(
    string? FilterData,
    long? BranchId,
    string? SeasonCode,
    string? SeasonName,
    long? CompanyId,
    int PageIndex,
    int PageSize) : IQuery<DataResult<List<GetsActiveSeasonModel>>?>;