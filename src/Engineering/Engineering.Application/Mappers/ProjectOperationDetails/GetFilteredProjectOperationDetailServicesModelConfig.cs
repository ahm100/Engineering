using Engineering.Application.Services.ContractorContracts.Contracts.GetsIntegratedProjectOperationDetailService;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Mappers;

public class GetsIntegratedProjectOperationDetailServiceModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProjectOperationDetailContractorService, GetsIntegratedProjectOperationDetailServiceModel>()
           // .Map(d => d.Id, s => s.Id)
           //.Map(d => d.OperationInfoCode, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode)
           //.Map(d => d.OperationInfoName, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName)
           //  .Map(d => d.OperationWorkload, s => s.ProjectOperationDetail.ProjectOperation.Workload)
           .Map(d => d.ServiceInfoName, s => s.OperationInfoService.ServiceInfo.ServiceInfoName)
           .Map(d => d.ServiceInfoCode, s => s.OperationInfoService.ServiceInfo.ServiceInfoCode)
           .Map(d => d.ServiceInfoVolume, s => s.Volume)
           //  .Map(d => d.OperationLocationPublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName)
           // .Map(d => d.OperationLocationPublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
           .Map(d => d.ContractorId, s => s.ContractorId)
           .Map(d => d.StartDate, s => s.ProjectOperationDetail.StartDate)
           .Map(d => d.EndDate, s => s.ProjectOperationDetail.EndDate)
           .Map(d => d.HasExperts, s => s.ProjectOperationDetailContractorExperts.Any())
           ;
    }
}
