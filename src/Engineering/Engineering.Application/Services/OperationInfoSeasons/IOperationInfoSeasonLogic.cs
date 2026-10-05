using Engineering.Application.Services.OperationInfoSeasons.Models.CreateOperationInfoSeason;
using Engineering.Application.Services.OperationInfoSeasons.Models.CreateOperationInfoSeasonFormGoodsSupply;
using Engineering.Application.Services.OperationInfoSeasons.Models.GetsByOperationInfoId;
using Engineering.Application.Services.OperationInfoSeasons.Models.GetsOperationInfoSeasonByProjectOperationId;

namespace Engineering.Application.Services.OperationInfoSeasons;

public interface IOperationInfoSeasonLogic
{
    Task<Result<CreateOperationInfoSeasonResponse?>> CreateOperationInfoSeason(
        CreateOperationInfoSeasonModelRequest request, CT ct);

    Task<Result<CreateOperationInfoSeasonFormGoodsSupplyResponse?>> CreateOperationInfoSeasonFormGoodsSupply(
        CreateOperationInfoSeasonFormGoodsSupplyRequest request, CT ct);

    Task<Result<GetsOperationInfoSeasonByIdResponse?>> GetsByOperationInfoId(
        GetsOperationInfoSeasonByIdRequest request, CT ct);

    Task<Result<GetsOperationInfoSeasonByProjectOperationIdResponse?>> GetsOperationInfoSeasonByProjectOperationId(
        GetsOperationInfoSeasonByProjectOperationIdRequest request, CT ct);
}