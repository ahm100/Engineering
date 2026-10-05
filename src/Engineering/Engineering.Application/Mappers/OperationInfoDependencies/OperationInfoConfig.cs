using Engineering.Application.Services.OperationInfos.Models.GetsPrioritizeOperationInfo;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Responses;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.ConsumptionStandards;

namespace Engineering.Application.Mappers;

public class OperationInfoConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OperationInfo, GetsPrioritizeOperationInfoResponseModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.OperationInfoName, s => s.OperationInfoName)
            .Map(d => d.OperationInfoCode, s => s.OperationInfoCode)
            .Map(d => d.OperationLatinName, s => s.OperationLatinName)
            .Map(d => d.Priority, s => s.Priority)
            .Map(d => d.IsActive, s => s.IsActive)
            ;

        config.NewConfig<OperationInfoGoodsDataModel, ConsumptionStandardProduct>()
             .Map(d => d.Number, s => s.GoodsNumber)
             .Map(d => d.UnusedPercentage, s => s.UnusedPercentage)
             .Map(d => d.StandardProductType, s => s.StandardProductType)
             .Map(d => d.ProductUnitId, s => s.Id);

        config.NewConfig<OperationInfoExpertDataModel, ConsumptionStandardExpert>()
            .Map(d => d.UnusedPercentage, s => s.UnusedPercentage)
            .Map(d => d.ExpertUnitId, s => s.Id)
            .Map(d => d.TimeSpant, s => s.TimeSpant)
            .Map(d => d.ExpertNumber, s => s.ExpertNumber);

        config.NewConfig<OperationInfoService, ServiceInfoDataModel>()
            .Map(d => d.OperationInfoServiceId, s => s.Id)
            .Map(d => d.Id, s => s.ServiceInfo.Id)
            .Map(d => d.ServiceInfoName, s => s.ServiceInfo.ServiceInfoName)
            .Map(d => d.ServiceInfoCode, s => s.ServiceInfo.ServiceInfoCode)
            .Map(d => d.IsActive, s => s.ServiceInfo.IsActive);

    }
}
