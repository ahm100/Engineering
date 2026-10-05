using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Responses;

namespace Engineering.Application.Services.ConsumptionStandards.Models.ConsumptionStandards.GetsConsumptionStandardByOperationInfoId;

public record GetsConsumptionStandardByOperationInfoIdResponse(
    List<OperationInfoExpertDataModel?> ExpertData,
    List<OperationInfoGoodsDataModel?> GoodsData,
    List<OperationInfoMachineryDataModel?> MachineryData
    );
