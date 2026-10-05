using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewardsExcelExporter;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Mappers.RequestRewards;

public class RequestRewardsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        ///TODO Employers
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<RequestReward, GetFilteredRequestRewardsExcelExporterResponseModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.ManagerDescription, s => s.ManagerDescription)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.ProjectId, s => s.Project.Id)
            .Map(d => d.CostCenterId, s => s.CostCenter.Id)
            .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
            .Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetail.Id)
            .Map(d => d.Status, s => s.Status)
            .Map(d => d.StatusDescription, s => s.Status.GetEnumDescription())
            .Map(d => d.Type, s => s.Type)
            .Map(d => d.TypeDescription, s => s.Type.GetEnumDescription())
            .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
            .Map(d => d.OperationInfoCode, s => s.ProjectOperation.OperationInfo.OperationInfoCode)
            .Map(d => d.ProjectName, s => s.Project.ProjectName)
            .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName)
            .Map(d => d.CurrencyId, s => s.CurrencyId)
            //.Map(d => d.ContractCode, s => s.ProjectOperation.EmployerContract.Code)
            .Map(d => d.PublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
            .Map(d => d.PublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName)
            .Map(d => d.RegistrationDate, s => TimeCalculator.ConvertToShamsi(s.RegistrationDate))
            .Map(d => d.OfferedPrice, s => s.OfferedPrice)
            .Map(d => d.ConfirmedPrice, s => s.ConfirmedPrice)
            .Map(d => d.CompanyId, s => s.CompanyId)
            ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        ;
    }
}