using Engineering.Application.Services.Seasons.Models.SeasonModels;

namespace Engineering.Application.Services.Seasons.Models.GetSeasons;

public record GetSeasonsResponse(
    List<GetSeasonsModel> Data,
    int RowCount);
