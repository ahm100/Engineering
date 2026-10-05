using Engineering.Application.Services.OperationInfoSeasons.Models.GetsByOperationInfoId;

namespace Engineering.Application.Services.OperationInfoSeasons.Models.GetsOperationInfoSeasonByProjectOperationId;

public record GetsOperationInfoSeasonByProjectOperationIdResponse(
    List<OperationInfoSeasonsCategoryModel>? CategoryData,
    List<OperationInfoSeasonsBranchModel>? BranchData,
    List<OperationInfoSeasonsSeasonModel>? SeasonData
    );
