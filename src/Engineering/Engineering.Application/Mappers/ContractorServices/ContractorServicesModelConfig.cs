using Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorsByServiceIds;
using Engineering.Application.Services.Contractors.Models.ContractorServices.GetContractorServicesByContractorId;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredByIds;
using Engineering.Domain.Entities.ContractorServices;

namespace Engineering.Application.Mappers.Branchs;

public class ContractorServicesModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ContractorService, GetContractorServicesByContractorIdModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.ServiceInfo, s => s.Adapt<GetContractorServicesByContractorIdServiceInfoModel>());

        config.NewConfig<ContractorService, GetContractorServicesByContractorIdServiceInfoModel>()
           .Map(d => d.Id, s => s.ServiceInfo.Id)
           .Map(d => d.ServiceInfoName, s => s.ServiceInfo.ServiceInfoName)
           .Map(d => d.ServiceInfoCode, s => s.ServiceInfo.ServiceInfoCode)
           .Map(d => d.UnitOfMeasurementId, s => s.ServiceInfo.UnitOfMeasurementId)
           .Map(d => d.IsActive, s => s.IsActive);

        config.NewConfig<FilteredUserModel, GetContractorsByServiceIdsModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.FullName, s => s.FirstName + " " + s.LastName)
           .Map(d => d.OrganizationCode, s => s.OrganizationCode)
           .Map(d => d.DefaultPhoneNo, s => s.DefaultPhoneNo)
           .Map(d => d.IsActive, s => s.IsActive);
    }
}