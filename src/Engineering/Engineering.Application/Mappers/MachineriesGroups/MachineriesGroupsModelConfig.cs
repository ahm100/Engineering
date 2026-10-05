using Engineering.Application.Services.MachineriesGroups.Models.CreateMachineriesGroup;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupByCode;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupById;
using Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupByName;
using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineriesGroupExcelExporter;
using Engineering.Application.Services.MachineriesGroups.Models.GetsMachineryGroupsForRequestMachinery;
using Engineering.Application.Services.MachineriesGroups.Models.MachineriesGroupModels;
using Engineering.Application.Services.MachineriesGroups.Models.UpdateMachineriesGroup;
using Engineering.Domain.Entities.Machineries;

namespace Engineering.Application.Mappers.MachineriesGroups;

public class MachineriesGroupsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MachineriesGroup, CreateMachineriesGroupResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.GroupName, s => s.GroupName)
           .Map(d => d.GroupCode, s => s.GroupCode)
           .Map(d => d.IsActive, s => s.IsActive);

        config.NewConfig<MachineriesGroup, UpdateMachineriesGroupResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.GroupName, s => s.GroupName)
           .Map(d => d.GroupCode, s => s.GroupCode)
           .Map(d => d.IsActive, s => s.IsActive);

        config.NewConfig<MachineriesGroup, GetMachineriesGroupByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.GroupName, s => s.GroupName)
           .Map(d => d.GroupCode, s => s.GroupCode)
          .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.IsActive, s => s.IsActive);

        config.NewConfig<MachineriesGroup, GetsMachineryGroupsForRequestMachineryModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.GroupName, s => s.GroupName)
           .Map(d => d.GroupCode, s => s.GroupCode);

        config.NewConfig<MachineriesGroup, GetMachineriesGroupByNameResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.GroupName, s => s.GroupName)
           .Map(d => d.GroupCode, s => s.GroupCode)
          .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.IsActive, s => s.IsActive);

        config.NewConfig<MachineriesGroup, GetMachineriesGroupByCodeResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.GroupName, s => s.GroupName)
           .Map(d => d.GroupCode, s => s.GroupCode)
          .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.IsActive, s => s.IsActive);

        config.NewConfig<MachineriesGroup, GetMachineriesGroupsWithChildModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.GroupName, s => s.GroupName)
           .Map(d => d.GroupCode, s => s.GroupCode)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.HaveChild, s => s.Machineries.Any())
           .Map(d => d.ChildCount, s => s.Machineries.Count());

        config.NewConfig<MachineriesGroup, GetsMachineriesGroupExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.GroupName, s => s.GroupName)
           .Map(d => d.GroupCode, s => s.GroupCode)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<MachineriesGroup, GetsActiveMachineriesGroupModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.GroupName, s => s.GroupName)
           .Map(d => d.GroupCode, s => s.GroupCode)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
    }
}
