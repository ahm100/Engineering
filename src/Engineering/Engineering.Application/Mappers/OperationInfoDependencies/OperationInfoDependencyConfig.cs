using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;
using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Responses;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.ConsumptionStandards;

namespace Engineering.Application.Mappers;

public class OperationInfoDependencyConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OperationInfo, GetOperationInfosModel>()
            .Map(d => d.Priority, s => s.Priority)
            //.Map(d => d.SeasonId, s => s.Season.Id)
            //.Map(d => d.SeasonName, s => s.Season.SeasonName)
            //.Map(d => d.BranchId, s => s.Season.Branch.Id)
            //.Map(d => d.BranchName, s => s.Season.Branch.BranchName)
            //.Map(d => d.CategoryId, s => s.Season.Branch.Category.Id)
            //.Map(d => d.CategoryName, s => s.Season.Branch.Category.CategoryName)
            .Map(d => d.OperationInfoDependencyId, s => s.OperationInfoDependencies.Any() ? s.OperationInfoDependencies.First().Id : (long?)null)
            .Map(d => d.RelationId, s => s.OperationInfoDependencies.Any() ? s.OperationInfoDependencies.First().RelationId : (long?)null)
            .Map(d => d.DependencyName, s => s.OperationInfoDependencies.Any() ? s.OperationInfoDependencies.First().OperationInfo.OperationInfoName : string.Empty)
            .Map(d => d.DependencyCode, s => s.OperationInfoDependencies.Any() ? s.OperationInfoDependencies.First().OperationInfo.OperationInfoCode : string.Empty)
            .Map(d => d.WorkingDays, s => s.OperationInfoDependencies.Any() ? s.OperationInfoDependencies.First().WorkingDays : (int?)null)
            .Map(d => d.DependencyType, s => s.OperationInfoDependencies.Any() ? s.OperationInfoDependencies.First().DependencyType.GetEnumDescription() : string.Empty)
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
