using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyHistoriesExcelExporter;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistories;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Mappers.DailyProjectOperations;

public class GetDailyProjectOperationHistoriesDetailModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<DailyProjectOperation, GetDailyProjectOperationHistoriesDetailModel>()
           .Map(d => d.DailyProjectOperationId, s => s.Id)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.Type, s => s.Type)
           .Map(d => d.UnitOfMeasurementId, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.UnitOfMeasurementId)
           .Map(d => d.Length, s => s.Length)
           .Map(d => d.Width, s => s.Width)
           .Map(d => d.Height, s => s.Height)
           .Map(d => d.Weight, s => s.Weight)
           .Map(d => d.Number, s => s.Number)
           .Map(d => d.FinalAmount, s => s.FinalAmount)
           .Map(d => d.Volume,
                s => s.DailyProjectOperationServices
                .OrderByDescending(x => x.Created)
                .Sum(x => x.Volume)
            )
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.CreatorId, s => s.CreatorId)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetail.Id)
           .Map(d => d.PrivateCode, s => s.ProjectOperationDetail.OperationLocation.PrivateCode)
           .Map(d => d.PrivateName, s => s.ProjectOperationDetail.OperationLocation.PrivateName)
           .Map(d => d.PublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
           .Map(d => d.PublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName)
           .Map(d => d.ProjectOperationDetailDescription, s => s.ProjectOperationDetail.Description)
           .Map(d => d.Created, s => TimeCalculator.DatePiker(s.Created))
           .Map(d => d.DocumentUrls, s => s.DailyProjectOperationDocuments.Adapt<List<GetDailyProjectOperationHistoriesDetailDocumentModel>>());

        config.NewConfig<DailyProjectOperation, GetDailyHistoriesExcelExporterResponseModel>()
           .Map(d => d.DailyProjectOperationId, s => s.Id)
           .Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetail.Id)
           .Map(d => d.PrivateCode, s => s.ProjectOperationDetail.OperationLocation.PrivateCode)
           .Map(d => d.PrivateName, s => s.ProjectOperationDetail.OperationLocation.PrivateName)
           .Map(d => d.PublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
           .Map(d => d.PublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName)
           .Map(d => d.ProjectOperationDetailDescription, s => s.ProjectOperationDetail.Description)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperationDetail.ProjectOperation.Id)
           .Map(d => d.OperationInfoName, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName)
           .Map(d => d.ProjectId, s => s.ProjectOperationDetail.ProjectOperation.Project.Id)
           .Map(d => d.ProjectName, s => s.ProjectOperationDetail.ProjectOperation.Project.ProjectName)
           .Map(d => d.CostCenterId, s => s.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenterId)
           .Map(d => d.CostCenterName, s => s.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName)
           .Map(d => d.StartDate, s => TimeCalculator.ConvertToShamsi(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.ConvertToShamsi(s.EndDate))
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.StatusDescription, s => s.Status.GetEnumDescription())
           .Map(d => d.UnitOfMeasurementId, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.UnitOfMeasurementId)
           .Map(d => d.FinalAmount, s => s.FinalAmount)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.Created, s => TimeCalculator.ConvertToShamsi(s.Created))
           .Map(d => d.Description, s => s.Description);
    }
}
