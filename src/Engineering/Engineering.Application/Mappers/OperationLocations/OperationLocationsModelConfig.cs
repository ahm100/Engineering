using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByCode;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationById;
using Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByName;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelExporter;
using Engineering.Application.Services.OperationLocations.Models.OperationLocationModels;
using Engineering.Domain.Entities.OperationLocations;

namespace Engineering.Application.Mappers.OperationLocations;

public class OperationLocationsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<OperationLocation, GetOperationLocationsWithChildModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.PrivateName, s => s.PrivateName)
           .Map(d => d.PrivateCode, s => s.PrivateCode)
           .Map(d => d.PublicName, s => s.PublicName)
           .Map(d => d.PublicCode, s => s.PublicCode)
           .Map(d => d.Path, s => s.Path)
           .Map(d => d.Coding, s => s.Coding)
           .Map(d => d.ParentId, s => s.ParentId)
           .Map(d => d.CostCenterId, s => s.CostCenterId)
           .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName)
           .Map(d => d.ProjectId, s => s.ProjectId)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.OperationLocationInfo, s => $"{s.PrivateCode}-{s.PrivateName}-{s.Priority ?? 0}")
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.HaveChild, s => s.Children.Any())
           .Map(d => d.ChildCount, s => s.Children.Count());

        config.NewConfig<OperationLocation, GetsOperationLocationExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.PrivateName, s => s.PrivateName)
           .Map(d => d.PrivateCode, s => s.PrivateCode)
           .Map(d => d.PublicName, s => s.PublicName)
           .Map(d => d.PublicCode, s => s.PublicCode)
           .Map(d => d.Path, s => s.Path)
           .Map(d => d.Coding, s => s.Coding)
           .Map(d => d.ParentId, s => s.ParentId)
           .Map(d => d.CostCenterId, s => s.CostCenterId)
           .Map(d => d.ProjectId, s => s.ProjectId)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.OperationLocationInfo, s => $"{s.PrivateCode}-{s.PrivateName}-{s.Priority ?? 0}")
           .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName)
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.HaveChild, s => s.Children.Any())
           .Map(d => d.ChildCount, s => s.Children.Count());

        config.NewConfig<OperationLocation, GetsOperationLocationByCostCenterIdModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.CostCenterId, s => s.CostCenterId)
           .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName)
           .Map(d => d.ProjectId, s => s.ProjectId)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.ParentId, s => s.ParentId)
           .Map(d => d.Coding, s => s.Coding)
           .Map(d => d.PrivateName, s => s.PrivateName)
           .Map(d => d.PrivateCode, s => s.PrivateCode)
           .Map(d => d.PublicName, s => s.PublicName)
           .Map(d => d.PublicCode, s => s.PublicCode)
          .Map(d => d.OperationLocationInfo, s => $"{s.PrivateCode}-{s.PrivateName}-{s.Priority ?? 0}")
           .Map(d => d.Path, s => s.Path)
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.HaveChild, s => s.Children.Any())
           .Map(d => d.ChildCount, s => s.Children.Count());

        config.NewConfig<OperationLocation, GetOperationLocationByCodeResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.PrivateName, s => s.PrivateName)
           .Map(d => d.PrivateCode, s => s.PrivateCode)
           .Map(d => d.PublicName, s => s.PublicName)
           .Map(d => d.PublicCode, s => s.PublicCode)
           .Map(d => d.Path, s => s.Path)
           .Map(d => d.Coding, s => s.Coding)
           .Map(d => d.ParentId, s => s.ParentId)
           .Map(d => d.CostCenterId, s => s.CostCenterId)
           .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName)
           .Map(d => d.ProjectId, s => s.ProjectId)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.OperationLocationInfo, s => $"{s.PrivateCode}-{s.PrivateName}-{s.Priority ?? 0}")
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.IsActive, s => s.IsActive)
           ;

        config.NewConfig<OperationLocation, GetOperationLocationByNameResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.PrivateName, s => s.PrivateName)
           .Map(d => d.PrivateCode, s => s.PrivateCode)
           .Map(d => d.PublicName, s => s.PublicName)
           .Map(d => d.PublicCode, s => s.PublicCode)
           .Map(d => d.Path, s => s.Path)
           .Map(d => d.Coding, s => s.Coding)
           .Map(d => d.ParentId, s => s.ParentId)
           .Map(d => d.CostCenterId, s => s.CostCenterId)
           .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName)
           .Map(d => d.ProjectId, s => s.ProjectId)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.OperationLocationInfo, s => $"{s.PrivateCode}-{s.PrivateName}-{s.Priority ?? 0}")
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.IsActive, s => s.IsActive)
           ;

        config.NewConfig<OperationLocation, GetOperationLocationByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.PrivateName, s => s.PrivateName)
           .Map(d => d.PrivateCode, s => s.PrivateCode)
           .Map(d => d.PublicName, s => s.PublicName)
           .Map(d => d.PublicCode, s => s.PublicCode)
           .Map(d => d.Path, s => s.Path)
           .Map(d => d.Coding, s => s.Coding)
           .Map(d => d.ParentId, s => s.ParentId)
           .Map(d => d.CostCenterId, s => s.CostCenterId)
           .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName)
           .Map(d => d.ProjectId, s => s.ProjectId)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.OperationLocationInfo, s => $"{s.PrivateCode}-{s.PrivateName}-{s.Priority ?? 0}")
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.CompanyId, s => s.CompanyId)
           ;

        config.NewConfig<OperationLocation, GetOperationLocationByCodeResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.PrivateName, s => s.PrivateName)
           .Map(d => d.PrivateCode, s => s.PrivateCode)
           .Map(d => d.PublicName, s => s.PublicName)
           .Map(d => d.PublicCode, s => s.PublicCode)
           .Map(d => d.Path, s => s.Path)
           .Map(d => d.Coding, s => s.Coding)
           .Map(d => d.ParentId, s => s.ParentId)
           .Map(d => d.CostCenterId, s => s.CostCenterId)
           .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName)
           .Map(d => d.ProjectId, s => s.ProjectId)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.OperationLocationInfo, s => $"{s.PrivateCode}-{s.PrivateName}-{s.Priority ?? 0}")
           .Map(d => d.IsActive, s => s.IsActive)
           ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

    }
}