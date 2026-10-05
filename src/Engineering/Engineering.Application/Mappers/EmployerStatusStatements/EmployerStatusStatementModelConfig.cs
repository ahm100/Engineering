using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementById;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementExcelExporter;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementExcelExporter;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementHistory;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperation;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationDetail;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationDetailDaily;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsFilteredEmployerStatusStatement;
using Engineering.Domain.Entities.EmployerStatusStatements;

namespace Engineering.Application.Mappers.EmployerStatusStatements;

public class EmployerStatusStatementModelConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<EmployerStatusStatement, GetsFilteredEmployerStatusStatementResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.SendStatusType, s => s.Status)
           .Map(d => d.SendStatusTypeTitle, s => s.Status.GetEnumDescription())
           .Map(d => d.StartDate, s => TimeCalculator.DatePiker(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.DatePiker(s.EndDate))
           .Map(d => d.StatusStatementCode, s => $"{s.StatusStatementCode}-{s.Id}")
           .Map(d => d.EmployerId, s => s.Project.EmployerId)
           .Map(d => d.ProjectId, s => s.Project.Id)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.CostCenterId,
           s => s.Project.ProjectCostCenters
           .Select(x => (long?)x.CostCenterId)
           .FirstOrDefault())

           .Map(d => d.CostCenterName,
           s => s.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())
           .Map(d => d.EmployerContractId, s => s.EmployerContract.Id)
           .Map(d => d.EmployerContractCode, s => s.EmployerContract.Code)
           .Map(d => d.EContractType, s => s.EmployerContract.EmployerContractHead.Type)
           .Map(d => d.EContractTypeDescription, s => s.EmployerContract.EmployerContractHead.Type.GetEnumDescription())
           .Map(d => d.PercentageOfWorkDone, s => s.PercentageOfWorkDone)
           .Map(d => d.CalculatedAmount, s => Math.Round(s.CalculatedAmount, 2))
           .Map(d => d.CurrencyId, s => s.EmployerContract.EmployerContractHead.CurrencyId)
           .Map(d => d.Urls, s => s.EmployerStatusStatementDocuments.Select(x => x.Url).ToList())
           .Map(d => d.Created, s => TimeCalculator.DatePiker(s.Created))
           ;

        config.NewConfig<EmployerStatusStatement, GetEmployerStatusStatementByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.SendStatusType, s => s.Status)
           .Map(d => d.SendStatusTypeTitle, s => s.Status.GetEnumDescription())
           .Map(d => d.StartDate, s => TimeCalculator.DatePiker(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.DatePiker(s.EndDate))
           .Map(d => d.StatusStatementCode, s => $"{s.StatusStatementCode}-{s.Id}")
           .Map(d => d.EmployerId, s => s.Project.EmployerId)
           .Map(d => d.ProjectId, s => s.Project.Id)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.CostCenterId,
           s => s.Project.ProjectCostCenters
           .Select(x => (long?)x.CostCenterId)
           .FirstOrDefault())
           .Map(d => d.CostCenterName,
           s => s.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())
           .Map(d => d.EmployerContractId, s => s.EmployerContract.Id)
           .Map(d => d.EmployerContractCode, s => s.EmployerContract.Code)
           .Map(d => d.EContractType, s => s.EmployerContract.EmployerContractHead.Type)
           .Map(d => d.PercentageOfWorkDone, s => Math.Round(s.PercentageOfWorkDone, 2))
           .Map(d => d.CalculatedAmount, s => Math.Round(s.CalculatedAmount, 2))
           .Map(d => d.CurrencyId, s => s.EmployerContract.EmployerContractHead.CurrencyId)
           .Map(d => d.Urls, s => s.EmployerStatusStatementDocuments.Select(x => x.Url).ToList())
           .Map(d => d.Created, s => TimeCalculator.DatePiker(s.Created))
           ;

        config.NewConfig<EmployerStatusStatement, GetEmployerStatusStatementExcelExporterResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.SendStatusType, s => s.Status)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.SendStatusTypeTitle, s => s.Status.GetEnumDescription())
           .Map(d => d.StartDate, s => TimeCalculator.ConvertToShamsi(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.ConvertToShamsi(s.EndDate))
           .Map(d => d.StatusStatementCode, s => $"{s.StatusStatementCode}-{s.Id}")
           .Map(d => d.EmployerId, s => s.Project.EmployerId)
           .Map(d => d.ProjectId, s => s.Project.Id)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.CostCenterId,
           s => s.Project.ProjectCostCenters
           .Select(x => (long?)x.CostCenterId)
           .FirstOrDefault())
           .Map(d => d.CostCenterName,
           s => s.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())
           .Map(d => d.EmployerContractId, s => s.EmployerContract.Id)
           .Map(d => d.EmployerContractCode, s => s.EmployerContract.Code)
           .Map(d => d.PercentageOfWorkDone, s => Math.Round(s.PercentageOfWorkDone, 2))
           .Map(d => d.CalculatedAmount, s => Math.Round(s.CalculatedAmount, 2))
           .Map(d => d.CurrencyId, s => s.EmployerContract.EmployerContractHead.CurrencyId)
           .Map(d => d.Urls, s => s.EmployerStatusStatementDocuments.Select(x => x.Url).ToList())
           .Map(d => d.Created, s => TimeCalculator.ConvertToShamsi(s.Created))
           ;

        config.NewConfig<EmployerStatusStatement, GetsEmployerStatusStatementExcelExporterResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.CompanyId, s => s.CompanyId)
           .Map(d => d.SendStatusType, s => s.Status)
           .Map(d => d.SendStatusTypeTitle, s => s.Status.GetEnumDescription())
           .Map(d => d.StartDate, s => TimeCalculator.ConvertToShamsi(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.ConvertToShamsi(s.EndDate))
           .Map(d => d.StatusStatementCode, s => $"{s.StatusStatementCode}-{s.Id}")
           .Map(d => d.EmployerId, s => s.Project.EmployerId)
           .Map(d => d.ProjectId, s => s.Project.Id)
           .Map(d => d.ProjectName, s => s.Project.ProjectName)
           .Map(d => d.CostCenterId,
           s => s.Project.ProjectCostCenters
           .Select(x => (long?)x.CostCenterId)
           .FirstOrDefault())
           .Map(d => d.CostCenterName,
           s => s.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())
           .Map(d => d.EmployerContractId, s => s.EmployerContract.Id)
           .Map(d => d.EmployerContractCode, s => s.EmployerContract.Code)
           .Map(d => d.PercentageOfWorkDone, s => Math.Round(s.PercentageOfWorkDone, 2))
           .Map(d => d.CalculatedAmount, s => Math.Round(s.CalculatedAmount, 2))
           .Map(d => d.CurrencyId, s => s.EmployerContract.EmployerContractHead.CurrencyId)
           .Map(d => d.Urls, s => s.EmployerStatusStatementDocuments.Select(x => x.Url).ToList())
           .Map(d => d.Created, s => TimeCalculator.ConvertToShamsi(s.Created))
           ;

        config.NewConfig<EmployerStatusStatementHistory, GetsEmployerStatusStatementHistoryResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.StatusDescription, s => s.Status.GetEnumDescription())
           .Map(d => d.StartDate, s => TimeCalculator.ConvertToShamsi(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.ConvertToShamsi(s.EndDate))
           .Map(d => d.PercentageOfWorkDone, s => Math.Round(s.PercentageOfWorkDone, 2))
           .Map(d => d.CalculatedAmount, s => Math.Round(s.CalculatedAmount, 2))
           .Map(d => d.Created, s => TimeCalculator.ConvertToShamsi(s.Created))
           ;

        config.NewConfig<EmployerStatusStatementProjectOperation, GetsEmployerStatusStatementProjectOperationResponseModel>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
            .Map(d => d.ProjectOperationStatus, s => s.ProjectOperation.ProjectOperationStatus)
            .Map(d => d.ProjectOperationStatusTitle, s => s.ProjectOperation.ProjectOperationStatus.GetEnumDescription())
            .Map(d => d.OperationInfoId, s => s.ProjectOperation.OperationInfo.Id)
            .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
            .Map(d => d.OperationInfoCode, s => s.ProjectOperation.OperationInfo.OperationInfoCode)
            .Map(d => d.StandardDeviation, s => Math.Round(s.ContractorWorkVolume, 2))
            .Map(d => d.TotalWorkVolume, s => Math.Round(s.ContractorWorkVolume, 2))
            .Map(d => d.DoneWorkVolume, s => Math.Round(s.ContractorWorkVolume, 2))
            .Map(d => d.StatusStatementWorkVolume, s => Math.Round(s.ContractorWorkVolume, 2))
            .Map(d => d.UnitOfMeasurementId, s => s.ProjectOperation.UnitOfMeasurementId)
            .Map(d => d.DonePercentage, s => Math.Round(s.ContractorWorkVolume, 2))
            .Map(d => d.TotalPercentage, s => Math.Round(s.ContractorWorkVolume, 2))
            .Map(d => d.CalculatedAmount, s => Math.Round(s.ContractorWorkVolume, 2))
            .Map(d => d.TotalDetailWorkVolume, s => s.TotalDetailWorkVolume)
            .Map(d => d.TotalDailyWorkVolume, s => s.TotalDailyWorkVolume)
            .Map(d => d.ContractorWorkVolume, s => s.ContractorWorkVolume)
            .Map(d => d.SupervisorWorkVolume, s => s.SupervisorWorkVolume)
            .Map(d => d.ConsultantWorkVolume, s => s.ConsultantWorkVolume)
            .Map(d => d.EmployerRepresentativeWorkVolume, s => s.EmployerRepresentativeWorkVolume)
            .Map(d => d.ContractorUnitPrice, s => s.ContractorUnitPrice)
            .Map(d => d.ContractorTotalPrice, s => s.ContractorTotalPrice)
            .Map(d => d.Urls, s => s.EmployerStatusStatementProjectOperationDocuments.Select(x => x.Url).ToList())
            .Map(d => d.ContractorConfirmeTotalPrice, s => s.ContractorConfirmeTotalPrice)
            .Map(d => d.EmployerCommercialUnitPrice, s => s.EmployerCommercialUnitPrice)
            .Map(d => d.EmployerCommercialTotalPrice, s => s.EmployerCommercialTotalPrice)
            .Map(d => d.EmployerCommercialConfirmeTotalPrice, s => s.EmployerCommercialConfirmeTotalPrice)
            .Map(d => d.Description, s => s.Description)
            ;

        config.NewConfig<EmployerStatusStatementProjectOperation, EmployerStatusStatementProjectOperations>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.ProjectOperationId, s => s.ProjectOperation.Id)
            .Map(d => d.ProjectOperationStatus, s => s.ProjectOperation.ProjectOperationStatus)
            .Map(d => d.ProjectOperationStatusTitle, s => s.ProjectOperation.ProjectOperationStatus.GetEnumDescription())
            .Map(d => d.OperationInfoId, s => s.ProjectOperation.OperationInfo.Id)
            .Map(d => d.OperationInfoName, s => s.ProjectOperation.OperationInfo.OperationInfoName)
            .Map(d => d.OperationInfoCode, s => s.ProjectOperation.OperationInfo.OperationInfoCode)
            .Map(d => d.StandardDeviation, s => Math.Round(s.ContractorWorkVolume, 2))
            .Map(d => d.TotalWorkVolume, s => Math.Round(s.TotalWorkVolume, 2))
            .Map(d => d.Urls, s => s.EmployerStatusStatementProjectOperationDocuments.Select(x => x.Url).ToList())
            .Map(d => d.DoneWorkVolume, s => Math.Round(s.ContractorWorkVolume, 2))
            .Map(d => d.StatusStatementWorkVolume, s => Math.Round(s.ContractorWorkVolume, 2))
            .Map(d => d.UnitOfMeasurementId, s => s.ProjectOperation.UnitOfMeasurementId)
            .Map(d => d.DonePercentage, s => Math.Round(s.ContractorWorkVolume, 2))
            .Map(d => d.TotalPercentage, s => Math.Round(s.ContractorWorkVolume, 2))
            .Map(d => d.CalculatedAmount, s => Math.Round(s.ContractorWorkVolume, 2))
            ;

        config.NewConfig<EmployerStatusStatementProjectOperationDetail, GetsEmployerStatusStatementProjectOperationDetailResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperationDetail.ProjectOperation.Id)
           .Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetail.Id)
           .Map(d => d.ProjectOperationDetailStatus, s => s.ProjectOperationDetail.Status)
           .Map(d => d.ProjectOperationDetailStatusTitle, s => s.ProjectOperationDetail.Status!.GetEnumDescription())
           .Map(d => d.OperationLocationId, s => s.ProjectOperationDetail.OperationLocation.Id)
           .Map(d => d.PrivateCode, s => s.ProjectOperationDetail.OperationLocation.PrivateName)
           .Map(d => d.PrivateName, s => s.ProjectOperationDetail.OperationLocation.PrivateCode)
           .Map(d => d.PublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName)
           .Map(d => d.PublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
           .Map(d => d.TotalWorkVolume, s => Math.Round(s.TotalWorkVolume, 2))
           .Map(d => d.TotalDailyWorkVolume, s => Math.Round(s.TotalDailyWorkVolume, 2))
           .Map(d => d.ContractorWorkVolume, s => Math.Round(s.ContractorWorkVolume, 2))
           .Map(d => d.SupervisorWorkVolume, s => Math.Round(s.SupervisorWorkVolume, 2))
           .Map(d => d.ConsultantWorkVolume, s => Math.Round(s.ConsultantWorkVolume, 2))
           .Map(d => d.EmployerRepresentativeWorkVolume, s => Math.Round(s.EmployerRepresentativeWorkVolume, 2))
           .Map(d => d.Description, s => s.Description)
           ;

        config.NewConfig<EmployerStatusStatementProjectOperationDetailDaily, GetsEmployerStatusStatementProjectOperationDetailDailyResponseModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectOperationId, s => s.DailyProjectOperation.ProjectOperationDetail.ProjectOperation.Id)
           .Map(d => d.ProjectOperationDetailId, s => s.DailyProjectOperation.ProjectOperationDetail.Id)
           .Map(d => d.ProjectOperationDetailStatus, s => s.DailyProjectOperation.ProjectOperationDetail.Status)
           .Map(d => d.ProjectOperationDetailStatusTitle, s => s.DailyProjectOperation.ProjectOperationDetail.Status!.GetEnumDescription())
           .Map(d => d.OperationLocationId, s => s.DailyProjectOperation.ProjectOperationDetail.OperationLocation.Id)
           .Map(d => d.PrivateCode, s => s.DailyProjectOperation.ProjectOperationDetail.OperationLocation.PrivateName)
           .Map(d => d.PrivateName, s => s.DailyProjectOperation.ProjectOperationDetail.OperationLocation.PrivateCode)
           .Map(d => d.PublicName, s => s.DailyProjectOperation.ProjectOperationDetail.OperationLocation.PublicName)
           .Map(d => d.PublicCode, s => s.DailyProjectOperation.ProjectOperationDetail.OperationLocation.PublicCode)
           .Map(d => d.Urls, s => s.EmployerStatusStatementDailyDocuments.Select(x => x.Url).ToList())
           .Map(d => d.ContractorLength, s => Math.Round(s.ContractorLength, 2))
           .Map(d => d.ContractorWidth, s => Math.Round(s.ContractorWidth, 2))
           .Map(d => d.ContractorWeight, s => Math.Round(s.ContractorWeight, 2))
           .Map(d => d.ContractorNumber, s => Math.Round(s.ContractorNumber, 2))
           .Map(d => d.ContractorVolume, s => Math.Round(s.ContractorVolume, 2))
           .Map(d => d.ContractorDescription, s => s.ContractorDescription)
           .Map(d => d.SupervisorLength, s => Math.Round(s.SupervisorLength, 2))
           .Map(d => d.SupervisorWidth, s => Math.Round(s.SupervisorWidth, 2))
           .Map(d => d.SupervisorWeight, s => Math.Round(s.SupervisorWeight, 2))
           .Map(d => d.SupervisorNumber, s => Math.Round(s.SupervisorNumber, 2))
           .Map(d => d.SupervisorVolume, s => Math.Round(s.SupervisorVolume, 2))
           .Map(d => d.SupervisorDescription, s => s.SupervisorDescription)
           .Map(d => d.ConsultantLength, s => Math.Round(s.ConsultantLength, 2))
           .Map(d => d.ConsultantWidth, s => Math.Round(s.ConsultantWidth, 2))
           .Map(d => d.ConsultantWeight, s => Math.Round(s.ConsultantWeight, 2))
           .Map(d => d.ConsultantNumber, s => Math.Round(s.ConsultantNumber, 2))
           .Map(d => d.ConsultantVolume, s => Math.Round(s.ConsultantVolume, 2))
           .Map(d => d.ConsultantDescription, s => s.ConsultantDescription)
           .Map(d => d.EmployerRepresentativeLength, s => Math.Round(s.EmployerRepresentativeLength, 2))
           .Map(d => d.EmployerRepresentativeWidth, s => Math.Round(s.EmployerRepresentativeWidth, 2))
           .Map(d => d.EmployerRepresentativeWeight, s => Math.Round(s.EmployerRepresentativeWeight, 2))
           .Map(d => d.EmployerRepresentativeNumber, s => Math.Round(s.EmployerRepresentativeNumber, 2))
           .Map(d => d.EmployerRepresentativeVolume, s => Math.Round(s.EmployerRepresentativeVolume, 2))
           .Map(d => d.EmployerRepresentativeDescription, s => s.EmployerRepresentativeDescription)
           ;

        config.NewConfig<EmployerStatusStatementProjectOperationDetail, EmployerStatusStatementProjectOperationDetails>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperationDetail.ProjectOperation.Id)
           .Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetail.Id)
           .Map(d => d.ProjectOperationDetailStatus, s => s.ProjectOperationDetail.Status)
           .Map(d => d.ProjectOperationDetailStatusTitle, s => s.ProjectOperationDetail.Status!.GetEnumDescription())
           .Map(d => d.OperationLocationId, s => s.ProjectOperationDetail.OperationLocation.Id)
           .Map(d => d.PrivateCode, s => s.ProjectOperationDetail.OperationLocation.PrivateName)
           .Map(d => d.PrivateName, s => s.ProjectOperationDetail.OperationLocation.PrivateCode)
           .Map(d => d.PublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName)
           .Map(d => d.PublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
           .Map(d => d.TotalWorkVolume, s => Math.Round(s.TotalWorkVolume, 2))
           .Map(d => d.DoneWorkVolume, s => Math.Round(s.ContractorWorkVolume, 2))
           .Map(d => d.StatusStatementWorkVolume, s => Math.Round(s.ContractorWorkVolume, 2))
           .Map(d => d.DonePercentage, s => Math.Round(s.ContractorWorkVolume, 2))
           .Map(d => d.TotalPercentage, s => Math.Round(s.ContractorWorkVolume, 2))
           .Map(d => d.CalculatedAmount, s => Math.Round(s.ContractorWorkVolume, 2))
           ;

    }
}
