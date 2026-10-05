using Engineering.Application.Services.ProjectTypes.Models.CreateProjectType;
using Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeByCode;
using Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeById;
using Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeByName;
using Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelExporter;
using Engineering.Application.Services.ProjectTypes.Models.ProjectTypeModels;
using Engineering.Application.Services.ProjectTypes.Models.UpdateProjectType;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Mappers.ProjectTypes;

public class ProjectTypesModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProjectType, CreateProjectTypeResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectTypeCode, s => s.ProjectTypeCode)
           .Map(d => d.ProjectTypeName, s => s.ProjectTypeTitle);

        config.NewConfig<ProjectType, UpdateProjectTypeResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectTypeCode, s => s.ProjectTypeCode)
           .Map(d => d.ProjectTypeName, s => s.ProjectTypeTitle);

        config.NewConfig<ProjectType, GetProjectTypeByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectTypeCode, s => s.ProjectTypeCode)
           .Map(d => d.ProjectTypeTitle, s => s.ProjectTypeTitle)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<ProjectType, GetProjectTypeByNameResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectTypeCode, s => s.ProjectTypeCode)
           .Map(d => d.ProjectTypeTitle, s => s.ProjectTypeTitle)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<ProjectType, GetProjectTypeByCodeResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectTypeCode, s => s.ProjectTypeCode)
           .Map(d => d.ProjectTypeTitle, s => s.ProjectTypeTitle)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;

        config.NewConfig<ProjectType, GetsProjectTypeExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectTypeName, s => s.ProjectTypeTitle)
           .Map(d => d.ProjectTypeCode, s => s.ProjectTypeCode)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<ProjectType, GetsProjectTypeModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectTypeTitle, s => s.ProjectTypeTitle)
           .Map(d => d.ProjectTypeCode, s => s.ProjectTypeCode)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<ProjectType, GetsActiveProjectTypeModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectTypeTitle, s => s.ProjectTypeTitle)
           .Map(d => d.ProjectTypeCode, s => s.ProjectTypeCode)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
    }
}