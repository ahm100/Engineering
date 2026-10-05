using Engineering.Application.Services.ProjectOperations.Models.GetsStatus;
using Engineering.Application.Services.Projects.Models.GetProjectProductCategoryByCostCenterId;
using Engineering.Application.Services.Projects.Models.GetProjectProductGroupByCostCenterId;
using Engineering.Application.Services.Projects.Models.GetsProjectByIds;
using Engineering.Application.Services.Projects.Models.GetsProjectExcelExporter;
using Engineering.Application.Services.Projects.Models.ProjectModels;
using Engineering.Application.WebServices.WarehouseServices.WarehouseAssetes.Queries.GetUnUsedWarehousesCategories;
using Engineering.Application.WebServices.WarehouseServices.WarehouseAssetes.Queries.GetUnUsedWarehousesGroups;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Mappers.ProjectTypes;

public class ProjectsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetUnUsedWarehousesCategoriesModel, GetProjectProductCategoryByCostCenterIdModel>()
           .Map(d => d.Id, s => s.CategoryId)
           .Map(d => d.CategoryId, s => s.CategoryId)
           .Map(d => d.Title, s => s.CategoryName)
           .Map(d => d.Code, s => s.CategoryCode)
           ;

        config.NewConfig<GetUnUsedWarehousesGroupsModel, GetProjectProductModel>()
           .Map(d => d.Id, s => s.GroupId)
           .Map(d => d.ProductGroupId, s => s.GroupId)
           .Map(d => d.ProductGroupName, s => s.GroupName)
           .Map(d => d.ProductGroupCode, s => s.GroupCode)
           ;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<Project, GetsProjectExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectTypeId, s => s.ProjectTypeId)
           .Map(d => d.ProjectTypeTitle, s => s.ProjectType.ProjectTypeTitle)
           .Map(d => d.ProjectName, s => s.ProjectName)
           .Map(d => d.ProjectCode, s => s.ProjectCode)
           .Map(d => d.EmployerId, s => s.EmployerId)

           .Map(d => d.CostCenterId,
           s => s.ProjectCostCenters
           .Select(x => (long?)x.CostCenterId)
           .FirstOrDefault())

           .Map(d => d.CostCenterName,
           s => s.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())
           .Map(d => d.SupervisorEngineer, s => s.SupervisorEngineer)
           .Map(d => d.AdvisorId, s => s.Advisor)
           .Map(d => d.ProjectManagerId, s => s.ProjectManager)
           .Map(d => d.PlanningAssistantId, s => s.PlanningAssistant)
           .Map(d => d.StatusTitle, s => s.Status.GetEnumDescription())
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.Contractual, s => s.Contractual)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        config.NewConfig<Project, GetContractorProjectsModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectName, s => s.ProjectName)
           .Map(d => d.ProjectCode, s => s.ProjectCode)
           .Map(d => d.Contractual, s => s.Contractual)
           ;
        config.NewConfig<Project, GetsActiveProjectModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectCode, s => s.ProjectCode)
           .Map(d => d.ProjectName, s => s.ProjectName)
           .Map(d => d.Contractual, s => s.Contractual)
           .Map(d => d.HasProduct, s => s.HasProduct)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;
        config.NewConfig<Project, GetProjectsByCostCenterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectCode, s => s.ProjectCode)
           .Map(d => d.ProjectName, s => s.ProjectName)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.Contractual, s => s.Contractual)
           .Map(d => d.HasProduct, s => s.HasProduct)
           .Map(d => d.IsActive, s => s.IsActive)
           ;
        config.NewConfig<Project, GetsProjectByIdsResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectCode, s => s.ProjectCode)
           .Map(d => d.Contractual, s => s.Contractual)
           .Map(d => d.ProjectName, s => s.ProjectName)
           .Map(d => d.HasProduct, s => s.HasProduct)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;

        config.NewConfig<ProjectStatus, ProjectStatusModel>()
           .Map(d => d.Code, s => (int)s)
           .Map(d => d.Description, s => s.GetEnumDescription());

    }
}