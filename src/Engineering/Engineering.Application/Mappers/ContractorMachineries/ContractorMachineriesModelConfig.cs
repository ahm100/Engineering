using Engineering.Application.Services.ContractorMachineries.Models.GetActiveContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineryById;
using Engineering.Application.Services.ContractorMachineries.Models.GetsByContractorId;
using Engineering.Domain.Entities.ContractorMachineries;

namespace Engineering.Application.Mappers.ContractorMachineries;

public class ContractorMachineriesModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ContractorMachinery, GetContractorMachineryByIdResponse>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.MachineryGroupId, s => s.Machinery.MachineriesGroup.Id)
            .Map(d => d.MachineryGroupName, s => s.Machinery.MachineriesGroup.GroupName)
            .Map(d => d.MachineryGroupCode, s => s.Machinery.MachineriesGroup.GroupCode)
            .Map(d => d.MachineryId, s => s.Machinery.Id)
            .Map(d => d.MachineryName, s => s.Machinery.MachineryName)
            .Map(d => d.MachineryCode, s => s.Machinery.MachineryCode)
            .Map(d => d.Unit, s => s.Unit)
            .Map(d => d.MachineryPrice, s => s.MachineryPrice)
            .Map(d => d.MachineryIdentifier, s => s.MachineryIdentifier)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.NumberPlates, s => s.NumberPlates)
            .Map(d => d.CurrencyId, s => s.CurrencyId)
            .Map(d => d.CreatorId, s => s.CreatorId)
            .Map(d => d.Created, s => s.Created)
            .Map(d => d.ContractorId, s => s.ContractorId)
            .Map(d => d.IsActive, s => s.IsActive)
            .Map(d => d.CompanyId, s => s.CompanyId);

        config.NewConfig<ContractorMachinery, GetsActiveContractorMachineryModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.MachineryGroupId, s => s.Machinery.MachineriesGroup.Id)
            .Map(d => d.MachineryGroupName, s => s.Machinery.MachineriesGroup.GroupName)
            .Map(d => d.MachineryGroupCode, s => s.Machinery.MachineriesGroup.GroupCode)
            .Map(d => d.MachineryId, s => s.Machinery.Id)
            .Map(d => d.MachineryName, s => s.Machinery.MachineryName)
            .Map(d => d.MachineryCode, s => s.Machinery.MachineryCode)
            .Map(d => d.Unit, s => s.Unit)
            .Map(d => d.MachineryPrice, s => s.MachineryPrice)
            .Map(d => d.MachineryIdentifier, s => s.MachineryIdentifier)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.NumberPlates, s => s.NumberPlates)
            .Map(d => d.CurrencyId, s => s.CurrencyId)
            .Map(d => d.CreatorId, s => s.CreatorId)
            .Map(d => d.Created, s => s.Created)
            .Map(d => d.ContractorId, s => s.ContractorId)
            .Map(d => d.IsActive, s => s.IsActive)
            .Map(d => d.CompanyId, s => s.CompanyId);

        config.NewConfig<ContractorMachinery, GetContractorMachineriesModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.MachineryGroupId, s => s.Machinery.MachineriesGroup.Id)
            .Map(d => d.MachineryGroupName, s => s.Machinery.MachineriesGroup.GroupName)
            .Map(d => d.MachineryGroupCode, s => s.Machinery.MachineriesGroup.GroupCode)
            .Map(d => d.MachineryId, s => s.Machinery.Id)
            .Map(d => d.MachineryName, s => s.Machinery.MachineryName)
            .Map(d => d.MachineryCode, s => s.Machinery.MachineryCode)
            .Map(d => d.Unit, s => s.Unit)
            .Map(d => d.MachineryPrice, s => s.MachineryPrice)
            .Map(d => d.MachineryIdentifier, s => s.MachineryIdentifier)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.NumberPlates, s => s.NumberPlates)
            .Map(d => d.CurrencyId, s => s.CurrencyId)
            .Map(d => d.CreatorId, s => s.CreatorId)
            .Map(d => d.Created, s => s.Created)
            .Map(d => d.ContractorId, s => s.ContractorId)
            .Map(d => d.IsActive, s => s.IsActive)
            .Map(d => d.CompanyId, s => s.CompanyId);

        config.NewConfig<ContractorMachinery, GetsByContractorIdModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.MachineryGroupId, s => s.Machinery.MachineriesGroup.Id)
            .Map(d => d.MachineryGroupName, s => s.Machinery.MachineriesGroup.GroupName)
            .Map(d => d.MachineryGroupCode, s => s.Machinery.MachineriesGroup.GroupCode)
            .Map(d => d.MachineryId, s => s.Machinery.Id)
            .Map(d => d.MachineryName, s => s.Machinery.MachineryName)
            .Map(d => d.MachineryCode, s => s.Machinery.MachineryCode)
            .Map(d => d.Unit, s => s.Unit)
            .Map(d => d.MachineryPrice, s => s.MachineryPrice)
            .Map(d => d.MachineryIdentifier, s => s.MachineryIdentifier)
            .Map(d => d.Description, s => s.Description)
            .Map(d => d.NumberPlates, s => s.NumberPlates)
            .Map(d => d.CurrencyId, s => s.CurrencyId)
            .Map(d => d.CreatorId, s => s.CreatorId)
            .Map(d => d.Created, s => s.Created)
            .Map(d => d.IsActive, s => s.IsActive)
            .Map(d => d.CompanyId, s => s.CompanyId);
    }

}