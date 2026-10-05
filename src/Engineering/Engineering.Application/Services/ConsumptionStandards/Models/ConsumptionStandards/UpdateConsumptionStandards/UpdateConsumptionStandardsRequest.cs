using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Requests;

namespace Engineering.Application.Services.ConsumptionStandards.Models.ConsumptionStandards.UpdateConsumptionStandards;

public record UpdateConsumptionStandardsRequest(
    long OprationInfoId,
    List<UpdateOperationInfoExpertRequestModel>? ExpertStandards,
    List<UpdateOperationInfoGoodsRequestModel>? GoodsStandards,
    List<UpdateOperationInfoMachineryRequestModel>? MachineryStandards
     ) : IHttpRequest;
