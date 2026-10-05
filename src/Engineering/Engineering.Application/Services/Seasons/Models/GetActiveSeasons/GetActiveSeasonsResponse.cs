using Engineering.Application.Services.Seasons.Models.SeasonModels;

namespace Engineering.Application.Services.Seasons.Models.GetActiveSeasons;

public record GetActiveSeasonsResponse(
    List<GetsActiveSeasonModel> Data,
    int RowCount
    );
