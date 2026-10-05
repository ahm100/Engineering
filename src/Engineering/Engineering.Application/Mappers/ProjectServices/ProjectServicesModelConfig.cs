using Engineering.Application.Services.ProjectServices.Models.GetProjectServiceById;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Mappers.ProjectServices;

public class ProjectServicesModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProjectService, GetProjectServiceByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ContractorId, s => s.ContractorId)

           .Map(d => d.CostCenterId,
           s => s.Project.ProjectCostCenters
           .Select(x => (long?)x.CostCenterId)
           .FirstOrDefault())

           .Map(d => d.CostCenterName,
           s => s.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())

           .Map(d => d.CostCenterCode,
           s => s.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterCode)
           .FirstOrDefault())
           .Map(d => d.ProjectId, s => s.Project.Id)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.ProjectCode, s => s.Project.ProjectCode)
           .Map(d => d.ServiceInfoId, s => s.ServiceInfo.Id)
           .Map(d => d.ServiceInfoName, s => s.ServiceInfo.ServiceInfoName)
           .Map(d => d.ServiceInfoCode, s => s.ServiceInfo.ServiceInfoCode)
           .Map(d => d.ServiceInfoMeasurId, s => s.ServiceInfo.UnitOfMeasurementId)
           .Map(d => d.IsActive, s => s.IsActive)
           .Map(d => d.Created, s => s.Created)
           ;
    }
}