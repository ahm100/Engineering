
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoSeasons.Models.CreateOperationInfoSeasonFormGoodsSupply;

public record CreateOperationInfoSeasonFormGoodsSupplyRequest(
    OperationInfo OperationInfo,
    long SeasonId
     ) : IHttpRequest;