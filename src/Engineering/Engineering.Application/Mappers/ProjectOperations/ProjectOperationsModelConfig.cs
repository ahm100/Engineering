using Engineering.Application.Services.ProjectOperations.Models.GetsFilteredByProjectIds;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReporting;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReportingExcelExporter;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Mappers.ProjectTypes;

public class ProjectOperationsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProjectOperation, GetsProjectOperationReportingModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.OperationInfoId, s => s.OperationInfo.Id)
           .Map(d => d.OperationInfoName, s => s.OperationInfo.OperationInfoName)
           .Map(d => d.OperationInfoCode, s => s.OperationInfo.OperationInfoCode)
           .Map(d => d.OperationInfoMeasurementId, s => s.OperationInfo.UnitOfMeasurementId)
           .Map(d => d.ProjectId, s => s.Project.Id)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.ProjectCode, s => s.Project.ProjectCode)

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
           .Map(d => d.MeasurementId, s => s.UnitOfMeasurementId)
           .Map(d => d.TolerancePercentage, s => s.TolerancePercentage)
           .Map(d => d.Price, s => s.Price)
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.Status, s => s.ProjectOperationStatus)
           .Map(d => d.Workload, s => s.Workload)
           .Map(d => d.CreatorId, s => s.CreatorId)
           .Map(d => d.GoodsInProgress, s => s.GoodsInProgress)
           .Map(d => d.Created, s => s.Created)
           .Map(d => d.Description, s => s.Description)
           ;

        config.NewConfig<ProjectOperation, GetsProjectOperationReportingExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.OperationInfoId, s => s.OperationInfo.Id)
           .Map(d => d.OperationInfoName, s => s.OperationInfo.OperationInfoName)
           .Map(d => d.OperationInfoCode, s => s.OperationInfo.OperationInfoCode)
           .Map(d => d.OperationInfoMeasurementId, s => s.OperationInfo.UnitOfMeasurementId)
           .Map(d => d.ProjectId, s => s.Project.Id)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.ProjectCode, s => s.Project.ProjectCode)

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

           .Map(d => d.MeasurementId, s => s.UnitOfMeasurementId)
           .Map(d => d.TolerancePercentage, s => s.TolerancePercentage)
           .Map(d => d.Price, s => s.Price)
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.Status, s => s.ProjectOperationStatus)
           .Map(d => d.GoodsInProgress, s => s.GoodsInProgress)
           .Map(d => d.StatusDescription, s => s.ProjectOperationStatus.GetEnumDescription())
           .Map(d => d.Workload, s => s.Workload)
           .Map(d => d.CreatorId, s => s.CreatorId)
           .Map(d => d.Created, s => TimeCalculator.ConvertToShamsi(s.Created))
           .Map(d => d.Description, s => s.Description)
           ;

        config.NewConfig<ProjectOperation, GetsFilteredByProjectIdsModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.OperationInfoId, s => s.OperationInfo.Id)
           .Map(d => d.OperationInfoName, s => s.OperationInfo.OperationInfoName)
           .Map(d => d.OperationInfoCode, s => s.OperationInfo.OperationInfoCode)
           .Map(d => d.OperationInfoMeasurementId, s => s.OperationInfo.UnitOfMeasurementId)
           .Map(d => d.MeasurementId, s => s.UnitOfMeasurementId)
           .Map(d => d.Workload, s => s.Workload)
           ;
    }
}