using Engineering.Application.Services.Machineries.Models.CreateMachinery;
using Engineering.Application.Services.Machineries.Models.GetMachineryByCode;
using Engineering.Application.Services.Machineries.Models.GetMachineryById;
using Engineering.Application.Services.Machineries.Models.GetMachineryByName;
using Engineering.Application.Services.Machineries.Models.GetsMachineryExcelExporter;
using Engineering.Application.Services.Machineries.Models.GetsMachineryForRequestMachinery;
using Engineering.Application.Services.Machineries.Models.MachineryModels;
using Engineering.Application.Services.Machineries.Models.UpdateMachinery;
using Engineering.Domain.Entities.Machineries;

namespace Engineering.Application.Mappers.Machineries;

public class MachineriesModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Machinery, CreateMachineryResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.MachineryCode, s => s.MachineryCode)
           .Map(d => d.MachineryName, s => s.MachineryName)
           .Map(d => d.GroupId, s => s.MachineriesGroup.Id)
           .Map(d => d.GroupName, s => s.MachineriesGroup.GroupName)
           .Map(d => d.GroupCode, s => s.MachineriesGroup.GroupCode)
           .Map(d => d.IsActive, s => s.IsActive);

        config.NewConfig<Machinery, UpdateMachineryResponse>()
           .Map(d => d.Id, s => s.Id);

        config.NewConfig<Machinery, GetMachineryByIdResponse>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.MachineryCode, s => s.MachineryCode)
            .Map(d => d.MachineryName, s => s.MachineryName)
            .Map(d => d.GroupId, s => s.MachineriesGroup.Id)
            .Map(d => d.GroupName, s => s.MachineriesGroup.GroupName)
            .Map(d => d.GroupCode, s => s.MachineriesGroup.GroupCode)
            .Map(d => d.IsActive, s => s.IsActive)
            .Map(d => d.CompanyId, s => s.CompanyId)
            .Map(d => d.MachineriesGroup, s => s.MachineriesGroup.Adapt<MachineriesGroupModel>());

        config.NewConfig<Machinery, GetMachineriesModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.MachineryCode, s => s.MachineryCode)
            .Map(d => d.MachineryName, s => s.MachineryName)
            .Map(d => d.GroupId, s => s.MachineriesGroup.Id)
            .Map(d => d.GroupName, s => s.MachineriesGroup.GroupName)
            .Map(d => d.GroupCode, s => s.MachineriesGroup.GroupCode)
            .Map(d => d.IsActive, s => s.IsActive)
            .Map(d => d.CompanyId, s => s.CompanyId)
            .Map(d => d.MachineriesGroup, s => s.MachineriesGroup.Adapt<MachineriesGroupModel>());

        config.NewConfig<Machinery, GetMachineryByNameResponse>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.MachineryCode, s => s.MachineryCode)
            .Map(d => d.MachineryName, s => s.MachineryName)
            .Map(d => d.CompanyId, s => s.CompanyId)
            .Map(d => d.GroupId, s => s.MachineriesGroup.Id)
            .Map(d => d.GroupName, s => s.MachineriesGroup.GroupName)
            .Map(d => d.GroupCode, s => s.MachineriesGroup.GroupCode)
            .Map(d => d.IsActive, s => s.IsActive);

        config.NewConfig<Machinery, GetMachineryByCodeResponse>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.MachineryCode, s => s.MachineryCode)
            .Map(d => d.CompanyId, s => s.CompanyId)
            .Map(d => d.MachineryName, s => s.MachineryName)
            .Map(d => d.GroupId, s => s.MachineriesGroup.Id)
            .Map(d => d.GroupName, s => s.MachineriesGroup.GroupName)
            .Map(d => d.GroupCode, s => s.MachineriesGroup.GroupCode)
            .Map(d => d.IsActive, s => s.IsActive);

        config.NewConfig<Machinery, GetsByMachineriesGroupIdModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.MachineryCode, s => s.MachineryCode)
            .Map(d => d.MachineryName, s => s.MachineryName)
            .Map(d => d.CompanyId, s => s.CompanyId)
            .Map(d => d.GroupId, s => s.MachineriesGroup.Id)
            .Map(d => d.GroupName, s => s.MachineriesGroup.GroupName)
            .Map(d => d.GroupCode, s => s.MachineriesGroup.GroupCode)
            .Map(d => d.IsActive, s => s.IsActive);

        config.NewConfig<Machinery, GetsMachineryForRequestMachineryModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.CompanyId, s => s.CompanyId)
            .Map(d => d.MachineryCode, s => s.MachineryCode)
            .Map(d => d.MachineryName, s => s.MachineryName);

        config.NewConfig<Machinery, GetsActiveMachineryModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.CompanyId, s => s.CompanyId)
            .Map(d => d.MachineryCode, s => s.MachineryCode)
            .Map(d => d.MachineryName, s => s.MachineryName)
            .Map(d => d.MachineryGroupId, s => s.MachineriesGroup.Id)
            .Map(d => d.MachineryGroupName, s => s.MachineriesGroup.GroupName)
            .Map(d => d.MachineryGroupCode, s => s.MachineriesGroup.GroupCode)
            ;

        config.NewConfig<Machinery, GetsMachineryExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.MachineryName, s => s.MachineryName)
           .Map(d => d.MachineryCode, s => s.MachineryCode)
           .Map(d => d.GroupId, s => s.MachineriesGroup.Id)
           .Map(d => d.GroupName, s => s.MachineriesGroup.GroupName)
           .Map(d => d.GroupCode, s => s.MachineriesGroup.GroupCode)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId);
    }

}