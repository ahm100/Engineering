using Engineering.Application.Services.Seasons.Models.SeasonModels;

namespace Engineering.Application.Services.Seasons.Models.GetsByBranchId;

public record GetsByBranchIdResponse(
    List<GetsByBranchIdModel> Data,
    bool HaveChild,
    int ChildCount,
    int RowCount);
