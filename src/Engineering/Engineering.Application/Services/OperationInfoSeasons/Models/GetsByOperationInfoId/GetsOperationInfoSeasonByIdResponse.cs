
namespace Engineering.Application.Services.OperationInfoSeasons.Models.GetsByOperationInfoId;

public record GetsOperationInfoSeasonByIdResponse(
    List<OperationInfoSeasonsCategoryModel>? CategoryData,
    List<OperationInfoSeasonsBranchModel>? BranchData,
    List<OperationInfoSeasonsSeasonModel>? SeasonData
    );
