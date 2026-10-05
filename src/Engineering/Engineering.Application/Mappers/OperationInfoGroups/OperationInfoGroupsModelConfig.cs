using Engineering.Application.Services.OperationInfoGroups.Models.CreateOperationInfoGroup;
using Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupByCode;
using Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupById;
using Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupByName;
using Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroupExcelExporter;
using Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupModels;
using Engineering.Application.Services.OperationInfoGroups.Models.UpdateOperationInfoGroup;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Mappers.OperationInfoGroups;

public class OperationInfoGroupsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OperationInfoGroup, CreateOperationInfoGroupResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.OperationInfoGroupCode, s => s.OperationInfoGroupCode)
           .Map(d => d.OperationInfoGroupName, s => s.OperationInfoGroupTitle);

        config.NewConfig<OperationInfoGroup, UpdateOperationInfoGroupResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.OperationInfoGroupCode, s => s.OperationInfoGroupCode)
           .Map(d => d.OperationInfoGroupName, s => s.OperationInfoGroupTitle);

        config.NewConfig<OperationInfoGroup, GetOperationInfoGroupByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.OperationInfoGroupCode, s => s.OperationInfoGroupCode)
           .Map(d => d.OperationInfoGroupTitle, s => s.OperationInfoGroupTitle)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId);

        config.NewConfig<OperationInfoGroup, GetOperationInfoGroupByNameResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.OperationInfoGroupCode, s => s.OperationInfoGroupCode)
           .Map(d => d.OperationInfoGroupTitle, s => s.OperationInfoGroupTitle)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId);

        config.NewConfig<OperationInfoGroup, GetOperationInfoGroupByCodeResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.OperationInfoGroupCode, s => s.OperationInfoGroupCode)
           .Map(d => d.OperationInfoGroupTitle, s => s.OperationInfoGroupTitle)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId);

        config.NewConfig<OperationInfoGroup, GetsOperationInfoGroupModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.OperationInfoGroupCode, s => s.OperationInfoGroupCode)
           .Map(d => d.OperationInfoGroupTitle, s => s.OperationInfoGroupTitle)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId);

        config.NewConfig<OperationInfoGroup, GetsActiveOperationInfoGroupModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.OperationInfoGroupCode, s => s.OperationInfoGroupCode)
           .Map(d => d.OperationInfoGroupTitle, s => s.OperationInfoGroupTitle)
           .Map(d => d.CompanyId, s => s.CompanyId);

        config.NewConfig<OperationInfoGroup, GetsOperationInfoGroupExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.OperationInfoGroupName, s => s.OperationInfoGroupTitle)
           .Map(d => d.OperationInfoGroupCode, s => s.OperationInfoGroupCode)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId);
    }
}