using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.AppointmentContractor;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByFilter;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByProjectOperationDetailId;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Mappers.ProjectOperationDetails;

public class ContractorServiceModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<ProjectOperationDetailContractorService, GetsContractorServiceByProjectOperationDetailIdModel>()
             .Map(d => d.Id, s => s.Id)
             .Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetail.Id)
             .Map(d => d.ServiceInfoId, s => s.OperationInfoService.ServiceInfo.Id)
             .Map(d => d.UnitOfMeasurementId, s => s.OperationInfoService.ServiceInfo.UnitOfMeasurementId)
             .Map(d => d.ServiceInfoName, s => s.OperationInfoService.ServiceInfo.ServiceInfoName)
             .Map(d => d.ServiceInfoCode, s => s.OperationInfoService.ServiceInfo.ServiceInfoCode)
             .Map(d => d.ContractorId, s => s.ContractorId)
             .Map(d => d.IsActive, s => s.IsActive)
             .Map(d => d.ProjectServiceDetailId, s => s.ProjectServiceDetail.Id)
             .Map(d => d.ProjectServiceId, s => s.ProjectServiceDetail.ProjectService.Id)
             .Map(d => d.ProjectServiceName, s => s.ProjectServiceDetail.ProjectService.ServiceInfo.ServiceInfoName)
             .Map(d => d.ProjectServiceUnitOfMeasurementId, s => s.ProjectServiceDetail.ProjectService.ServiceInfo.UnitOfMeasurementId)
             .Map(d => d.ProjectServiceVolume, s => s.ProjectServiceDetail.ProjectService.Volume)
             .Map(d => d.ProjectServiceDoneVolume, s => s.ProjectServiceDetail.ProjectService.DoneVolume)
             .Map(d => d.ProjectServiceRemaindVolume, s => s.ProjectServiceDetail.ProjectService.RemainderVolume)
             .Map(d => d.TimeSpant, s => TimeCalculator.TicksToStringHM(s.TimeSpant))
             .Map(d => d.Volume, s => s.Volume)
             .Map(d => d.UsedVolume, s => s.DailyOperationServices.Sum(x => x.Volume));

        config.NewConfig<ProjectOperationDetailContractorService, GetsContractorServiceByFilterModel>()
             .Map(d => d.Id, s => s.Id)
             .Map(d => d.ContractorId, s => s.ContractorId)
             .Map(d => d.OperationLocationId, s => s.ProjectOperationDetail.OperationLocation.Id)
             .Map(d => d.PublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName)
             .Map(d => d.PublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
             .Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetail.Id)
             .Map(d => d.ProjectOperationId, s => s.ProjectOperationDetail.ProjectOperation.Id)
             .Map(d => d.OperationInfoId, s => s.ProjectOperationDetail.ProjectOperation.Id)
             .Map(d => d.OperationInfoName, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName)
             .Map(d => d.OperationInfoCode, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode)
             .Map(d => d.ProjectServiceDetailId, s => s.ProjectServiceDetail.Id)
             .Map(d => d.ServiceInfoId, s => s.OperationInfoService.ServiceInfo.Id)
             .Map(d => d.ServiceInfoName, s => s.OperationInfoService.ServiceInfo.ServiceInfoName)
             .Map(d => d.ServiceInfoCode, s => s.OperationInfoService.ServiceInfo.ServiceInfoCode)
             .Map(d => d.Type, s => s.Type);

        config.NewConfig<ProjectOperationDetailContractorService, AppointmentContractorModel>()
             .Map(d => d.Id, s => s.Id)
             .Map(d => d.ContractorId, s => s.ContractorId)
             .Map(d => d.OperationLocationId, s => s.ProjectOperationDetail.OperationLocation.Id)
             .Map(d => d.PrivateName, s => s.ProjectOperationDetail.OperationLocation.PrivateName)
             .Map(d => d.PrivateCode, s => s.ProjectOperationDetail.OperationLocation.PrivateCode)
             .Map(d => d.PublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName)
             .Map(d => d.PublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
             .Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetail.Id)
             .Map(d => d.ProjectOperationId, s => s.ProjectOperationDetail.ProjectOperation.Id)
             .Map(d => d.OperationInfoId, s => s.ProjectOperationDetail.ProjectOperation.Id)
             .Map(d => d.ProjectServiceDetailId, s => s.ProjectServiceDetail.Id)
             .Map(d => d.OperationInfoName, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName)
             .Map(d => d.OperationInfoCode, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode)
             .Map(d => d.ServiceInfoId, s => s.OperationInfoService.ServiceInfo.Id)
             .Map(d => d.ServiceInfoName, s => s.OperationInfoService.ServiceInfo.ServiceInfoName)
             .Map(d => d.ServiceInfoCode, s => s.OperationInfoService.ServiceInfo.ServiceInfoCode);

#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}
