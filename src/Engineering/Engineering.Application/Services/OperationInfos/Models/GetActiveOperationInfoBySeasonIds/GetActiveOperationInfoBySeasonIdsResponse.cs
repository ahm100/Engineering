using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;

namespace Engineering.Application.Services.OperationInfos.Models.GetActiveOperationInfoBySeasonIds;

public record GetActiveOperationInfoBySeasonIdsResponse(
    List<GetsActiveOperationInfosModel> Data,
    int RowCount);

