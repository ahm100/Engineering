using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;
using Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsByProjectOperationIdExcelExporter;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReporting;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReportingExcelExporter;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsSummarizedByProjectOperationIds;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Mappers.ProjectOperationDetails;

public class ProjectOperationDetailModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProjectOperationDetail, GetsByExpertIdModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
           .Map(d => d.ProjectId, s => s.ProjectOperation.Project.Id)
           .Map(d => d.ProjectName, s => s.ProjectOperation.Project.ProjectName)
           .Map(d => d.OperationInfoId, s => s.ProjectOperation.OperationInfo.Id)
           .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
           .Map(d => d.StartDate, s => TimeCalculator.DatePiker(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.DatePiker(s.EndDate))
           .Map(d => d.Length, s => s.Length)
           .Map(d => d.LengthChangeable, s => s.LengthChangeable)
           .Map(d => d.Width, s => s.Width)
           .Map(d => d.WidthChangeable, s => s.WidthChangeable)
           .Map(d => d.Height, s => s.Height)
           .Map(d => d.HeightChangeable, s => s.HeightChangeable)
           .Map(d => d.Weight, s => s.Weight)
           .Map(d => d.WeightChangeable, s => s.WeightChangeable)
           .Map(d => d.Number, s => s.Number)
           .Map(d => d.NumberChangeable, s => s.NumberChangeable)
           .Map(d => d.FinalAmount, s => s.FinalAmount)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.StatusTitle, s => s.Status!.GetEnumDescription())
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.Day, s => s.Day)
           .Map(d => d.Hour, s => s.Hour)
           .Map(d => d.Description, s => s.Description);

        config.NewConfig<ConsumableVolumeExpert, ExpertDataModel>()
             .Map(d => d.Id, s => s.Id)
             .Map(d => d.ExpertId, s => s.ExpertId)
             .Map(d => d.Number, s => s.Number)
             .Map(d => d.UnusedPercentage, s => s.UnusedPercentage)
             .Map(d => d.IsStandard, s => s.IsStandard)
             .Map(d => d.IsStandardTitle, s => s.IsStandard ? "استاندارد" : "غیراستاندارد")
             .Map(d => d.StandardValue, s => TimeCalculator.TicksToStringHM((long)s.StandardValue!))
             .Map(d => d.FinalValue, s => TimeCalculator.TicksToStringHM(s.FinalValue));

        config.NewConfig<ProjectOperationDetail, GetsProjectOperationDetailReportingModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.Code, s => s.Code)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
           .Map(d => d.OperationInfoId, s => s.ProjectOperation.OperationInfo.Id)
           .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
           .Map(d => d.OperationInfoCode, s => s.ProjectOperation.OperationInfo.OperationInfoCode)
           .Map(d => d.MeasurementId, s => s.ProjectOperation.UnitOfMeasurementId)
           .Map(d => d.ProjectId, s => s.ProjectOperation.Project.Id)
           .Map(d => d.ProjectName, s => s.ProjectOperation.Project.ProjectName)
           .Map(d => d.ProjectCode, s => s.ProjectOperation.Project.ProjectCode)

           .Map(d => d.CostCenterId,
           s => s.ProjectOperation.Project.ProjectCostCenters
           .Select(x => (long?)x.CostCenterId)
           .FirstOrDefault())

           .Map(d => d.CostCenterName,
           s => s.ProjectOperation.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())

           .Map(d => d.CostCenterCode,
           s => s.ProjectOperation.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterCode)
           .FirstOrDefault())
           .Map(d => d.OperationLocationId, s => s.OperationLocation.Id)
           .Map(d => d.PrivateName, s => s.OperationLocation.PrivateName)
           .Map(d => d.PrivateCode, s => s.OperationLocation.PrivateCode)
           .Map(d => d.PublicName, s => s.OperationLocation.PublicName)
           .Map(d => d.PublicCode, s => s.OperationLocation.PublicCode)
           .Map(d => d.Length, s => s.Length)
           .Map(d => d.Width, s => s.Width)
           .Map(d => d.Height, s => s.Height)
           .Map(d => d.Weight, s => s.Weight)
           .Map(d => d.Number, s => s.Number)
           .Map(d => d.FinalAmount, s => s.FinalAmount)
           .Map(d => d.DoneFinalAmount, s => s.DailyOperations.Sum(x => x.FinalAmount))
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.StartDate, s => TimeCalculator.DatePiker(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.DatePiker(s.EndDate))
           .Map(d => d.Day, s => s.Day)
           .Map(d => d.Hour, s => s.Hour)
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.CreatorId, s => s.CreatorId)
           .Map(d => d.Created, s => TimeCalculator.DatePiker(s.Created))
           .Map(d => d.UpdaterId, s => s.UpdaterId)
           .Map(d => d.Updated, s => TimeCalculator.DatePiker(s.Updated))
           .Map(d => d.Description, s => s.Description);

#pragma warning disable CS8604 // Possible null reference argument.
        config.NewConfig<ProjectOperationDetail, GetsProjectOperationDetailReportingExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.Code, s => s.Code)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
           .Map(d => d.OperationInfoId, s => s.ProjectOperation.OperationInfo.Id)
           .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
           .Map(d => d.OperationInfoCode, s => s.ProjectOperation.OperationInfo.OperationInfoCode)
           .Map(d => d.MeasurementId, s => s.ProjectOperation.UnitOfMeasurementId)
           .Map(d => d.ProjectId, s => s.ProjectOperation.Project.Id)
           .Map(d => d.ProjectName, s => s.ProjectOperation.Project.ProjectName)
           .Map(d => d.ProjectCode, s => s.ProjectOperation.Project.ProjectCode)

           .Map(d => d.CostCenterId,
           s => s.ProjectOperation.Project.ProjectCostCenters
           .Select(x => (long?)x.CostCenterId)
           .FirstOrDefault())

           .Map(d => d.CostCenterName,
           s => s.ProjectOperation.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())

           .Map(d => d.CostCenterCode,
           s => s.ProjectOperation.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterCode)
           .FirstOrDefault())

           .Map(d => d.OperationLocationId, s => s.OperationLocation.Id)
           .Map(d => d.PrivateName, s => s.OperationLocation.PrivateName)
           .Map(d => d.PrivateCode, s => s.OperationLocation.PrivateCode)
           .Map(d => d.PublicName, s => s.OperationLocation.PublicName)
           .Map(d => d.PublicCode, s => s.OperationLocation.PublicCode)
           .Map(d => d.Length, s => s.Length)
           .Map(d => d.Width, s => s.Width)
           .Map(d => d.Height, s => s.Height)
           .Map(d => d.Weight, s => s.Weight)
           .Map(d => d.Number, s => s.Number)
           .Map(d => d.FinalAmount, s => s.FinalAmount)
           .Map(d => d.DoneFinalAmount, s => s.DailyOperations.Sum(x => x.FinalAmount))
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.StatusDescription, s => s.Status.GetEnumDescription())
           .Map(d => d.StartDate, s => TimeCalculator.ConvertToShamsi(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.ConvertToShamsi(s.EndDate))
           .Map(d => d.Day, s => s.Day)
           .Map(d => d.Hour, s => s.Hour)
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.CreatorId, s => s.CreatorId)
           .Map(d => d.Created, s => TimeCalculator.ConvertToShamsi(s.Created))
           .Map(d => d.UpdaterId, s => s.UpdaterId)
           .Map(d => d.Updated, s => TimeCalculator.ConvertToShamsi(s.Updated))
           .Map(d => d.Description, s => s.Description);
#pragma warning restore CS8604 // Possible null reference argument.

        config.NewConfig<ProjectOperationDetail, GetsByMachineryIdModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
           .Map(d => d.ProjectId, s => s.ProjectOperation.Project.Id)
           .Map(d => d.ProjectName, s => s.ProjectOperation.Project.ProjectName)
           .Map(d => d.OperationInfoId, s => s.ProjectOperation.OperationInfo.Id)
           .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
           .Map(d => d.StartDate, s => TimeCalculator.DatePiker(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.DatePiker(s.EndDate))
           .Map(d => d.Length, s => s.Length)
           .Map(d => d.LengthChangeable, s => s.LengthChangeable)
           .Map(d => d.Width, s => s.Width)
           .Map(d => d.WidthChangeable, s => s.WidthChangeable)
           .Map(d => d.Height, s => s.Height)
           .Map(d => d.HeightChangeable, s => s.HeightChangeable)
           .Map(d => d.Weight, s => s.Weight)
           .Map(d => d.WeightChangeable, s => s.WeightChangeable)
           .Map(d => d.Number, s => s.Number)
           .Map(d => d.NumberChangeable, s => s.NumberChangeable)
           .Map(d => d.FinalAmount, s => s.FinalAmount)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.StatusTitle, s => s.Status!.GetEnumDescription())
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.Day, s => s.Day)
           .Map(d => d.Hour, s => s.Hour)
           .Map(d => d.Description, s => s.Description);

        config.NewConfig<ConsumableVolumeMachinery, MachineryDataModel>()
             .Map(d => d.Id, s => s.Id)
             .Map(d => d.MachineryId, s => s.Machinery.Id)
             .Map(d => d.MachineryName, s => s.Machinery.MachineryName)
             .Map(d => d.MachineryCode, s => s.Machinery.MachineryCode)
             .Map(d => d.Number, s => s.Number)
             .Map(d => d.UnusedPercentage, s => s.UnusedPercentage)
             .Map(d => d.IsStandard, s => s.IsStandard)
             .Map(d => d.Unit, s => s.Unit)
             .Map(d => d.IsStandardTitle, s => s.IsStandard ? "استاندارد" : "غیراستاندارد")
             .Map(d => d.StandardValue, s => TimeCalculator.TicksToStringHM((long)s.StandardValue!))
             .Map(d => d.FinalValue, s => CastToStr(s))
             .Map(d => d.FinalValueDecimal, s => s.FinalValue)
             ;


        config.NewConfig<ProjectOperationDetail, GetsByProductIdModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
           .Map(d => d.ProjectId, s => s.ProjectOperation.Project.Id)
           .Map(d => d.ProjectName, s => s.ProjectOperation.Project.ProjectName)
           .Map(d => d.OperationInfoId, s => s.ProjectOperation.OperationInfo.Id)
           .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
           .Map(d => d.StartDate, s => TimeCalculator.DatePiker(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.DatePiker(s.EndDate))
           .Map(d => d.Length, s => s.Length)
           .Map(d => d.LengthChangeable, s => s.LengthChangeable)
           .Map(d => d.Width, s => s.Width)
           .Map(d => d.WidthChangeable, s => s.WidthChangeable)
           .Map(d => d.Height, s => s.Height)
           .Map(d => d.HeightChangeable, s => s.HeightChangeable)
           .Map(d => d.Weight, s => s.Weight)
           .Map(d => d.WeightChangeable, s => s.WeightChangeable)
           .Map(d => d.Number, s => s.Number)
           .Map(d => d.NumberChangeable, s => s.NumberChangeable)
           .Map(d => d.FinalAmount, s => s.FinalAmount)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.StatusTitle, s => s.Status!.GetEnumDescription())
           .Map(d => d.Priority, s => s.Priority)
           .Map(d => d.Day, s => s.Day)
           .Map(d => d.Hour, s => s.Hour)
           .Map(d => d.Description, s => s.Description);

        config.NewConfig<ConsumableVolumeProduct, ProductDataModel>()
             .Map(d => d.Id, s => s.Id)
             .Map(d => d.ProductGroupId, s => s.ProductGroupId)
             .Map(d => d.UnusedPercentage, s => s.UnusedPercentage)
             .Map(d => d.IsStandard, s => s.IsStandard)
             .Map(d => d.IsStandardTitle, s => s.IsStandard ? "استاندارد" : "غیراستاندارد")
             .Map(d => d.StandardValue, s => s.StandardValue!)
             .Map(d => d.VolumeProductType, s => s.VolumeProductType)
             .Map(d => d.TypeDescription, s => s.VolumeProductType.GetEnumDescription())
             ;

        config.NewConfig<ProjectOperationDetail, GetsSummarizedByProjectOperationIdsModel>()
             .Map(d => d.Id, s => s.Id)
             .Map(d => d.PrivateCode, s => s.OperationLocation.PrivateCode)
             .Map(d => d.PrivateName, s => s.OperationLocation.PrivateName);

#pragma warning disable CS8604 // Possible null reference argument.
        config.NewConfig<ProjectOperationDetail, GetsByProjectOperationIdExcelExporterResponseModel>()
             .Map(d => d.Id, s => s.Id)
             .Map(d => d.OperationLocationId, s => s.OperationLocation.Id)
             .Map(d => d.PublicName, s => s.OperationLocation.PublicName)
             .Map(d => d.PublicCode, s => s.OperationLocation.PublicCode)
             .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
             .Map(d => d.OperationInfoId, s => s.ProjectOperation.OperationInfo.Id)
             .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
             .Map(d => d.ProjectId, s => s.ProjectOperation.Project.Id)
             .Map(d => d.ProjectName, s => s.ProjectOperation.Project.ProjectName)

             .Map(d => d.CostCenterId,
             s => s.ProjectOperation.Project.ProjectCostCenters
             .Select(x => (long?)x.CostCenterId)
             .FirstOrDefault())

             .Map(d => d.CostCenterName,
             s => s.ProjectOperation.Project.ProjectCostCenters
             .Select(x => x.CostCenter.CostCenterName)
             .FirstOrDefault())

             .Map(d => d.ProjectManagerId, s => s.ProjectOperation.Project.ProjectManager)
             .Map(d => d.StartDate, s => TimeCalculator.ConvertToShamsi(s.StartDate))
             .Map(d => d.EndDate, s => TimeCalculator.ConvertToShamsi(s.EndDate))
             .Map(d => d.Length, s => s.Length)
             .Map(d => d.LengthChangeable, s => s.LengthChangeable)
             .Map(d => d.Width, s => s.Width)
             .Map(d => d.WidthChangeable, s => s.WidthChangeable)
             .Map(d => d.Height, s => s.Height)
             .Map(d => d.HeightChangeable, s => s.HeightChangeable)
             .Map(d => d.Weight, s => s.Weight)
             .Map(d => d.WeightChangeable, s => s.WeightChangeable)
             .Map(d => d.Number, s => s.Number)
             .Map(d => d.NumberChangeable, s => s.NumberChangeable)
             .Map(d => d.FinalAmount, s => s.FinalAmount)
             .Map(d => d.StatusDescription, s => s.Status.GetEnumDescription())
             .Map(d => d.Priority, s => s.Priority)
             .Map(d => d.Day, s => s.Day)
             .Map(d => d.Hour, s => s.Hour)
             .Map(d => d.Description, s => s.Description)
             .Map(d => d.CreatorId, s => s.CreatorId)
             .Map(d => d.CreateDate, s => TimeCalculator.ConvertToShamsi(s.Created))
             .Map(d => d.UpdatorId, s => s.UpdaterId)
             .Map(d => d.CompanyId, s => s.CompanyId)
             .Map(d => d.ModifyDate, s => TimeCalculator.ConvertToShamsi(s.Updated));
#pragma warning restore CS8604 // Possible null reference argument.

    }

    private static string CastToStr(ConsumableVolumeMachinery s)
    {
        return (Convert.ToInt64(s.FinalValue) > 1000000) ?
                TimeCalculator.TicksToStringHM(Convert.ToInt64(s.FinalValue)) : Convert.ToString(s.FinalValue);
    }
}
