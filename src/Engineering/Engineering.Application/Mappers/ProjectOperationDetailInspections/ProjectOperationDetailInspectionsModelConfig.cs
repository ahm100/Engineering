using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetFilteredProjectOperationDetailInspections;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetProjectOperationDetailInspectionById;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetTotalDailiesByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetailInspections.Models.ProjectOperationDetailInspectionDocumentModel;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Mappers.ProjectOperationDetailInspections;

public class ProjectOperationDetailInspectionsModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<ProjectOperationDetailInspection, GetFilteredProjectOperationDetailInspectionsModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetail.Id)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
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
           .Map(d => d.Length, s => s.Length)
           .Map(d => d.Width, s => s.Width)
           .Map(d => d.Height, s => s.Height)
           .Map(d => d.Weight, s => s.Weight)
           .Map(d => d.Number, s => s.Number)
           .Map(d => d.FinalAmount, s => s.FinalAmount)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.InspectionDate, s => s.InspectionDate)
           .Map(d => d.CreateDate, s => s.Created)
           .Map(d => d.CreatorId, s => s.CreatorId);

        config.NewConfig<ProjectOperationDetailInspection, GetProjectOperationDetailInspectionByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetail.Id)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
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
           .Map(d => d.Length, s => s.Length)
           .Map(d => d.Width, s => s.Width)
           .Map(d => d.Height, s => s.Height)
           .Map(d => d.Weight, s => s.Weight)
           .Map(d => d.Number, s => s.Number)
           .Map(d => d.FinalAmount, s => s.FinalAmount)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.InspectionDate, s => s.InspectionDate)
           .Map(d => d.CreateDate, s => s.Created)
           .Map(d => d.CreatorId, s => s.CreatorId);

        config.NewConfig<ProjectOperationDetail, GetTotalDailiesByProjectOperationDetailIdResponse>()
           .Map(d => d.ProjectOperationDetailId, s => s.Id)
           .Map(d => d.OperationLocationId, s => s.OperationLocation.Id)
           .Map(d => d.PublicName, s => s.OperationLocation.PublicName)
           .Map(d => d.PublicCode, s => s.OperationLocation.PublicCode)
           .Map(d => d.PrivateName, s => s.OperationLocation.PrivateName)
           .Map(d => d.PrivateCode, s => s.OperationLocation.PrivateCode)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
           .Map(d => d.OperationInfoId, s => s.ProjectOperation.OperationInfo.Id)
           .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
           .Map(d => d.OperationInfoCode, s => s.ProjectOperation.OperationInfo.OperationInfoCode)
           .Map(d => d.ProjectId, s => s.ProjectOperation.Project.Id)
           .Map(d => d.ProjectName, s => s.ProjectOperation.Project.ProjectName)
           .Map(d => d.ProjectCode, s => s.ProjectOperation.Project.ProjectCode)
           .Map(d => d.Description, s => s.Description);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        config.NewConfig<ProjectOperationDetailInspectionDocument, ProjectOperationDetailInspectionDocumentResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.Url, s => s.Url)
           ;
    }
}