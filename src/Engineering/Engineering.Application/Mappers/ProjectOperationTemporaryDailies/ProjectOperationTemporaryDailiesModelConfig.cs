using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetCurrentUserTemporaryDailies;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetFilteredProjectOperationTemporaryDailies;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.GetProjectOperationTemporaryDailyById;
using Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.ProjectOperationTemporaryDailyDocumentModel;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Mappers.ProjectOperationTemporaryDailies;

public class ProjectOperationTemporaryDailiesModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<ProjectOperationTemporaryDaily, GetFilteredProjectOperationTemporaryDailiesModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
           .Map(d => d.ProjectOperationName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
           .Map(d => d.ProjectOperationCode, s => s.ProjectOperation.OperationInfo.OperationInfoCode)
           .Map(d => d.CostCenterId, s => s.CostCenter.Id)
           .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName)
           .Map(d => d.CostCenterCode, s => s.CostCenter.CostCenterCode)
           .Map(d => d.ProjectId, s => s.Project.Id)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.ProjectCode, s => s.Project.ProjectCode)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.CreatorId, s => s.CreatorId);

        config.NewConfig<ProjectOperationTemporaryDaily, GetCurrentUserTemporaryDailiesModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
           .Map(d => d.ProjectOperationName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
           .Map(d => d.ProjectOperationCode, s => s.ProjectOperation.OperationInfo.OperationInfoCode)
           .Map(d => d.CostCenterId, s => s.CostCenter.Id)
           .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName)
           .Map(d => d.CostCenterCode, s => s.CostCenter.CostCenterCode)
           .Map(d => d.ProjectId, s => s.Project.Id)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.ProjectCode, s => s.Project.ProjectCode)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.CreatorId, s => s.CreatorId)
           ;

        config.NewConfig<ProjectOperationTemporaryDaily, GetProjectOperationTemporaryDailyByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
           .Map(d => d.ProjectOperationName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
           .Map(d => d.ProjectOperationCode, s => s.ProjectOperation.OperationInfo.OperationInfoCode)
           .Map(d => d.CostCenterId, s => s.CostCenter.Id)
           .Map(d => d.CostCenterName, s => s.CostCenter.CostCenterName)
           .Map(d => d.CostCenterCode, s => s.CostCenter.CostCenterCode)
           .Map(d => d.ProjectId, s => s.Project.Id)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.ProjectCode, s => s.Project.ProjectCode)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.CreatorId, s => s.CreatorId)
           ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        config.NewConfig<ProjectOperationTemporaryDailyDocument, ProjectOperationTemporaryDailyDocumentResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.Url, s => s.Url)
           ;
    }
}