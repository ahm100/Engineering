
namespace Engineering.Application.Services.OperationInfos.Models.GetsBySeasonId;

public record GetsBySeasonIdResponse(
    List<GetsBySeasonIdModel?> Data,
    int RowCount);
