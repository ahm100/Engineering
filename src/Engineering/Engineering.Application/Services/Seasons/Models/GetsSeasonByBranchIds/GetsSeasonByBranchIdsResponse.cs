
namespace Engineering.Application.Services.Seasons.Models.GetsSeasonByBranchIds;

public record GetsSeasonByBranchIdsResponse(
    List<GetsSeasonByBranchIdsModel> Data,
    bool HaveChild,
    int ChildCount,
    int RowCount);
