using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractHeaderInfo;
using Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractHistory;
using Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetFilteredContractorContractsByContractor;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractReports.Exporter;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractDetailReports;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractReports;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedOperationContract;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsRequestedServiceContract;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Mappers;

public class ContractorContractsConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProjectOperationDetailContractorService, GetsRequestedOperationContractModel>()
           .Map(d => d.ProjectOperationDetailServiceId, s => s.Id)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperationDetail.ProjectOperation.Id)
           .Map(d => d.OperationInfoId, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.Id)
           .Map(d => d.OperationInfoCode, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode)
           .Map(d => d.OperationInfoName, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName)
           .Map(d => d.OperationWorkload, s => s.ProjectOperationDetail.ProjectOperation.Workload)
           .Map(d => d.UnitOfMeasurementId, s => s.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId);

        config.NewConfig<ProjectOperationDetailContractorService, GetsRequestedServiceContractModel>()
           //.Map(d => d.ProjectOperationDetailServiceId, s => s.Id)
           .Map(d => d.ServiceInfoId, s => s.OperationInfoService.ServiceInfo.Id)
           .Map(d => d.ServiceInfoName, s => s.OperationInfoService.ServiceInfo.ServiceInfoName)
           .Map(d => d.ServiceInfoCode, s => s.OperationInfoService.ServiceInfo.ServiceInfoCode)
           .Map(d => d.ServiceInfoVolume, s => s.Volume)
           //.Map(d => d.OperationLocationPublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName)
           //.Map(d => d.OperationLocationPublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
           .Map(d => d.StartDate, s => TimeCalculator.DatePiker(s.ProjectOperationDetail.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.DatePiker(s.ProjectOperationDetail.EndDate))
           .Map(d => d.UnitOfMeasurementId, s => s.OperationInfoService.ServiceInfo.UnitOfMeasurementId)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperationDetail.ProjectOperation.Id)
           //.Map(d => d.OperationInfoName, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName)
           //.Map(d => d.OperationInfoCode, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode)
           //.Map(d => d.OperationInfoUnitOfMeasurementId, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.UnitOfMeasurementId)
           //.Map(d => d.ProjectOperationUnitOfMeasurementId, s => s.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId)
           //.Map(d => d.OperationWorkload, s => s.ProjectOperationDetail.ProjectOperation.Workload)
           ;

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        //config.NewConfig<ContractorContractDetail, GetContractorContractHeaderByIdDetailModel>()
        //.Map(d => d.Id, s => s.Id)
        //.Map(d => d.ProjectOperationId, s => s.ProjectOperation!.Id, opt => opt.ProjectOperation != null)
        //.Map(d => d.ProjectOperationId, s => s.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Id, opt => opt.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService != null)
        //.Map(d => d.ProjectOperationId, s => 0)

        //.Map(d => d.OperationInfoName, s => s.ProjectOperation!.OperationInfo.OperationInfoName, opt => opt.ProjectOperation != null)
        //.Map(d => d.OperationInfoName, s => s.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName, opt => opt.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService != null)
        //.Map(d => d.OperationInfoName, s => string.Empty)

        //.Map(d => d.OperationInfoCode, s => s.ProjectOperation!.OperationInfo.OperationInfoCode, opt => opt.ProjectOperation != null)
        //.Map(d => d.OperationInfoCode, s => s.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode, opt => opt.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService != null)
        //.Map(d => d.OperationInfoCode, s => string.Empty)

        //.Map(d => d.ProjectOperationUnitOfMeasurementId, s => s.ProjectOperation!.UnitOfMeasurementId, opt => opt.ProjectOperation != null)
        //.Map(d => d.ProjectOperationUnitOfMeasurementId, s => s.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId, opt => opt.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService != null)
        //.Map(d => d.ProjectOperationUnitOfMeasurementId, s => 0)

        //.Map(d => d.PublicCode, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.OperationLocation.PublicCode)
        //.Map(d => d.PublicName, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.OperationLocation.PublicName)
        //.Map(d => d.ServiceInfoName, s => s.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.ServiceInfoName)
        //.Map(d => d.ServiceInfoCode, s => s.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.ServiceInfoCode)
        //.Map(d => d.ServiceInfoUnitOfMeasurementId, s => s.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.UnitOfMeasurementId)
        //.Map(d => d.Volume, s => s.ProjectOperationDetailContractorService!.Volume)
        //.Map(d => d.ServiceInfoId, s => s.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.Id)

        //.Map(d => d.Workload, s => s.WorkLoad)
        //.Map(d => d.StartDate, s => s.StartDate)
        //.Map(d => d.EndDate, s => s.EndDate)
        //.Map(d => d.UnitAmount, s => s.UnitAmount)
        //.Map(d => d.TotalAmount, s => s.TotalAmount)
        //.Map(d => d.Services, s => s.ContractorContractDetailServices.Adapt<List<GetContractorContractHeaderByIdDetailServiceModel>>())
        //.Map(d => d.Prices, s => s.ContractorContractDetailPrices.Adapt<List<GetContractorContractHeaderByIdDetailPriceModel>>())
        //;

        config.NewConfig<ContractorContractDetail, GetsFilteredContractorContractDetailReportsModel>()
           .Map(d => d.ContractorContractId, s => s.ContractorContract.Id)
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.WorkLoad, s => s.WorkLoad)
           .Map(d => d.StartDate, s => TimeCalculator.DatePiker(s.StartDate), opt => opt.StartDate != null)
           .Map(d => d.EndDate, s => TimeCalculator.DatePiker(s.EndDate), opt => opt.EndDate != null)
           .Map(d => d.UnitAmount, s => s.UnitAmount)
           .Map(d => d.TotalAmount, s => s.TotalAmount)

            //.Map(d => d.ProjectOperationId, s => s.ProjectOperation!.Id, opt => opt.ProjectOperation != null)
            //.Map(d => d.ProjectOperationId, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Id, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.ProjectOperationId, s => 0)

            //.Map(d => d.OperationInfoId, s => s.ProjectOperation!.OperationInfo.Id, opt => opt.ProjectOperation != null)
            //.Map(d => d.OperationInfoId, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.OperationInfo.Id, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.OperationInfoId, s => 0)

            //.Map(d => d.OperationInfoName, s => s.ProjectOperation!.OperationInfo.OperationInfoName, opt => opt.ProjectOperation != null)
            //.Map(d => d.OperationInfoName, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.OperationInfoName, s => string.Empty)

            //.Map(d => d.OperationInfoCode, s => s.ProjectOperation!.OperationInfo.OperationInfoCode, opt => opt.ProjectOperation != null)
            //.Map(d => d.OperationInfoCode, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.OperationInfoCode, s => string.Empty)

            //.Map(d => d.ProjectId, s => s.ProjectOperation!.Project.Id, opt => opt.ProjectOperation != null)
            //.Map(d => d.ProjectId, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.Id, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.ProjectId, s => 0)

            //.Map(d => d.ProjectName, s => s.ProjectOperation!.Project.ProjectName, opt => opt.ProjectOperation != null)
            //.Map(d => d.ProjectName, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.ProjectName, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.ProjectName, s => string.Empty)

            //.Map(d => d.ProjectCode, s => s.ProjectOperation!.Project.ProjectCode, opt => opt.ProjectOperation != null)
            //.Map(d => d.ProjectCode, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.ProjectCode, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.ProjectCode, s => string.Empty)

            //.Map(d => d.CostCenterId, s => s.ProjectOperation!.Project.CostCenter.Id, opt => opt.ProjectOperation != null)
            //.Map(d => d.CostCenterId, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.CostCenter.Id, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.CostCenterId, s => 0)

            //.Map(d => d.CostCenterName, s => s.ProjectOperation!.Project.CostCenter.CostCenterName, opt => opt.ProjectOperation != null)
            //.Map(d => d.CostCenterName, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.CostCenter.CostCenterName, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.CostCenterName, s => string.Empty)

            //.Map(d => d.CostCenterCode, s => s.ProjectOperation!.Project.CostCenter.CostCenterCode, opt => opt.ProjectOperation != null)
            //.Map(d => d.CostCenterCode, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.CostCenter.CostCenterCode, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.CostCenterCode, s => string.Empty)

            //.Map(d => d.ServiceInfoId, s => s.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.Id, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.ServiceInfoId, s => 0)
            //.Map(d => d.ServiceInfoName, s => s.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.ServiceInfoName, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.ServiceInfoName, s => string.Empty)
            //.Map(d => d.ServiceInfoCode, s => s.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.ServiceInfoCode, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.ServiceInfoCode, s => string.Empty)
            //.Map(d => d.ServiceInfoUnitOfMeasurementId, s => s.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.UnitOfMeasurementId, opt => opt.ProjectOperationDetailContractorService != null)
            //.Map(d => d.ServiceInfoUnitOfMeasurementId, s => 0)

            //.Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.Id)
            //.Map(d => d.StatusDescription, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.Status.GetEnumDescription())
            //.Map(d => d.Status, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.Status)
            //.Map(d => d.OperationLocationId, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.OperationLocation.Id)
            //.Map(d => d.PublicCode, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.OperationLocation.PublicCode)
            //.Map(d => d.PublicName, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.OperationLocation.PublicName)
            //.Map(d => d.PrivateCode, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.OperationLocation.PrivateCode)
            //.Map(d => d.PrivateName, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.OperationLocation.PrivateName)
            ;

        config.NewConfig<ContractorContractDetail, GetsContractorContractDetailReportsExcelExporterModel>()
           .Map(d => d.ContractorContractId, s => s.ContractorContract.Id)
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.WorkLoad, s => s.WorkLoad)
           .Map(d => d.StartDate, s => TimeCalculator.ConvertToShamsi(s.StartDate), opt => opt.StartDate != null)
           .Map(d => d.EndDate, s => TimeCalculator.ConvertToShamsi(s.EndDate), opt => opt.EndDate != null)
           .Map(d => d.UnitAmount, s => s.UnitAmount)
           .Map(d => d.TotalAmount, s => s.TotalAmount)

           //.Map(d => d.ProjectOperationId, s => s.ProjectOperation!.Id, opt => opt.ProjectOperation != null)
           //.Map(d => d.ProjectOperationId, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Id, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.ProjectOperationId, s => 0)

           //.Map(d => d.OperationInfoId, s => s.ProjectOperation!.OperationInfo.Id, opt => opt.ProjectOperation != null)
           //.Map(d => d.OperationInfoId, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.OperationInfo.Id, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.OperationInfoId, s => 0)

           //.Map(d => d.OperationInfoName, s => s.ProjectOperation!.OperationInfo.OperationInfoName, opt => opt.ProjectOperation != null)
           //.Map(d => d.OperationInfoName, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.OperationInfoName, s => string.Empty)

           //.Map(d => d.OperationInfoCode, s => s.ProjectOperation!.OperationInfo.OperationInfoCode, opt => opt.ProjectOperation != null)
           //.Map(d => d.OperationInfoCode, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.OperationInfoCode, s => string.Empty)

           //.Map(d => d.ProjectId, s => s.ProjectOperation!.Project.Id, opt => opt.ProjectOperation != null)
           //.Map(d => d.ProjectId, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.Id, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.ProjectId, s => 0)

           //.Map(d => d.ProjectName, s => s.ProjectOperation!.Project.ProjectName, opt => opt.ProjectOperation != null)
           //.Map(d => d.ProjectName, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.ProjectName, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.ProjectName, s => string.Empty)

           //.Map(d => d.ProjectCode, s => s.ProjectOperation!.Project.ProjectCode, opt => opt.ProjectOperation != null)
           //.Map(d => d.ProjectCode, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.ProjectCode, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.ProjectCode, s => string.Empty)

           //.Map(d => d.CostCenterId, s => s.ProjectOperation!.Project.CostCenter.Id, opt => opt.ProjectOperation != null)
           //.Map(d => d.CostCenterId, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.CostCenter.Id, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.CostCenterId, s => 0)

           //.Map(d => d.CostCenterName, s => s.ProjectOperation!.Project.CostCenter.CostCenterName, opt => opt.ProjectOperation != null)
           //.Map(d => d.CostCenterName, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.CostCenter.CostCenterName, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.CostCenterName, s => string.Empty)

           //.Map(d => d.CostCenterCode, s => s.ProjectOperation!.Project.CostCenter.CostCenterCode, opt => opt.ProjectOperation != null)
           //.Map(d => d.CostCenterCode, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.CostCenter.CostCenterCode, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.CostCenterCode, s => string.Empty)

           //.Map(d => d.ServiceInfoId, s => s.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.Id, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.ServiceInfoId, s => 0)
           //.Map(d => d.ServiceInfoName, s => s.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.ServiceInfoName, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.ServiceInfoName, s => string.Empty)
           //.Map(d => d.ServiceInfoCode, s => s.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.ServiceInfoCode, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.ServiceInfoCode, s => string.Empty)
           //.Map(d => d.ServiceInfoUnitOfMeasurementId, s => s.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.UnitOfMeasurementId, opt => opt.ProjectOperationDetailContractorService != null)
           //.Map(d => d.ServiceInfoUnitOfMeasurementId, s => 0)

           //.Map(d => d.ProjectOperationDetailId, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.Id)
           //.Map(d => d.StatusDescription, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.Status.GetEnumDescription())
           //.Map(d => d.Status, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.Status)
           //.Map(d => d.OperationLocationId, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.OperationLocation.Id)
           //.Map(d => d.PublicCode, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.OperationLocation.PublicCode)
           //.Map(d => d.PublicName, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.OperationLocation.PublicName)
           //.Map(d => d.PrivateCode, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.OperationLocation.PrivateCode)
           //.Map(d => d.PrivateName, s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.OperationLocation.PrivateName)
           .Map(d => d.Created, s => TimeCalculator.ConvertToShamsi(s.Created))

;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.

        config.NewConfig<ContractorContract, GetFilteredContractorContractsByContractorModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ContractorNumber, s => s.Id)
           .Map(d => d.Created, s => s.Created);

        config.NewConfig<ContractorContractHeader, GetsFilteredContractorContractHeaderModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ContractorId, s => s.ContractorId)
           .Map(d => d.HaveStatusStatement, s => s.ContractorContracts.Any(x => x.ContractorStatusStatementDetails.Any()))
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.CurrencyId, s => s.CurrencyId)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.FinalTotalAmount, s => s.FinalTotalAmount)
           .Map(d => d.TotalPercentageDoingJobWell, s => s.TotalPercentageDoingJobWell)
           .Map(d => d.TotalDoingJobWellAmount, s => s.TotalDoingJobWellAmount)
           .Map(d => d.TotalPercentageAdvancePayment, s => s.TotalPercentageAdvancePayment)
           .Map(d => d.TotalAdvancePaymentAmount, s => s.TotalAdvancePaymentAmount)
           .Map(d => d.TotalDailyLatenessPenalty, s => s.TotalDailyLatenessPenalty)
           .Map(d => d.TotalWorkDonePercent, s => s.TotalWorkDonePercent)
           .Map(d => d.TotalWorkDeliveryPercent, s => s.TotalWorkDeliveryPercent)
           .Map(d => d.TotalWorkCompletionPercent, s => s.TotalWorkCompletionPercent)
           .Map(d => d.CreatorId, s => s.CreatorId)
           .Map(d => d.Created, s => s.Created)
           .Map(d => d.Description, s => s.Description);

        config.NewConfig<ContractorContract, GetsFilteredContractorContractReportsModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ContractorId, s => s.ContractorContractHeader.ContractorId)
           .Map(d => d.Status, s => s.ContractorContractHeader.Status)
           .Map(d => d.StatusDescription, s => s.ContractorContractHeader.Status.GetEnumDescription())
           .Map(d => d.CurrencyId, s => s.ContractorContractHeader.CurrencyId)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.TotalAmount, s => s.TotalAmount)
           .Map(d => d.PercentageDoingJobWell, s => s.PercentageDoingJobWell)
           .Map(d => d.DoingJobWellAmount, s => s.DoingJobWellAmount)
           .Map(d => d.PercentageAdvancePayment, s => s.PercentageAdvancePayment)
           .Map(d => d.AdvancePaymentAmount, s => s.AdvancePaymentAmount)
           .Map(d => d.DailyLatenessPenalty, s => s.DailyLatenessPenalty)
           .Map(d => d.WorkDonePercent, s => s.WorkDonePercent)
           .Map(d => d.WorkDeliveryPercent, s => s.WorkDeliveryPercent)
           .Map(d => d.WorkCompletionPercent, s => s.WorkCompletionPercent)
           .Map(d => d.CreatorId, s => s.CreatorId)
           .Map(d => d.Created, s => s.Created)
           .Map(d => d.Created, s => s.Created)
           .Map(d => d.ContractorContractTypeId, s => s.ContractorContractType)
           .Map(d => d.ContractorContractTypeName, s => s.ContractorContractType.GetEnumDescription())
           .Map(d => d.ContractorContractTypeCode, s => s.ContractorContractType)
           .Map(d => d.Description, s => s.ContractorContractHeader.Description);

        config.NewConfig<ContractorContract, GetsContractorContractReportsExcelExporterModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ContractorId, s => s.ContractorContractHeader.ContractorId)
           .Map(d => d.Status, s => s.ContractorContractHeader.Status)
           .Map(d => d.StatusDescription, s => s.ContractorContractHeader.Status.GetEnumDescription())
           .Map(d => d.CurrencyId, s => s.ContractorContractHeader.CurrencyId)
           .Map(d => d.StartDate, s => TimeCalculator.ConvertToShamsi(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.ConvertToShamsi(s.EndDate))
           .Map(d => d.TotalAmount, s => s.TotalAmount)
           .Map(d => d.PercentageDoingJobWell, s => s.PercentageDoingJobWell)
           .Map(d => d.DoingJobWellAmount, s => s.DoingJobWellAmount)
           .Map(d => d.PercentageAdvancePayment, s => s.PercentageAdvancePayment)
           .Map(d => d.AdvancePaymentAmount, s => s.AdvancePaymentAmount)
           .Map(d => d.DailyLatenessPenalty, s => s.DailyLatenessPenalty)
           .Map(d => d.WorkDonePercent, s => s.WorkDonePercent)
           .Map(d => d.WorkDeliveryPercent, s => s.WorkDeliveryPercent)
           .Map(d => d.WorkCompletionPercent, s => s.WorkCompletionPercent)
           .Map(d => d.CreatorId, s => s.CreatorId)
           .Map(d => d.Created, s => TimeCalculator.ConvertToShamsi(s.Created))
           .Map(d => d.ContractorContractTypeId, s => s.ContractorContractType)
           .Map(d => d.ContractorContractTypeName, s => s.ContractorContractType.GetEnumDescription())
           .Map(d => d.ContractorContractTypeCode, s => s.ContractorContractType)
           .Map(d => d.Description, s => s.ContractorContractHeader.Description);

        config.NewConfig<ContractorContractDetailPrice, GetContractorContractHeaderByIdDetailPriceModel>()
           .Map(d => d.Id, s => s.Id);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<ContractorContractDetailService, GetContractorContractHeaderByIdDetailServiceModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ProjectOperationDetailContractorServiceId, s => s.ProjectOperationDetailContractorService.Id)
           .Map(d => d.ProjectServiceId, s => s.ProjectOperationDetailContractorService.ProjectServiceDetail.ProjectService.Id)
           .Map(d => d.ProjectServiceDetailId, s => s.ProjectOperationDetailContractorService.ProjectServiceDetail.Id)
           .Map(d => d.ServiceInfoId, s => s.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id)
           .Map(d => d.ServiceInfoName, s => s.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName)
           .Map(d => d.ServiceInfoCode, s => s.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode)
           .Map(d => d.ServiceInfoUnitOfMeasurementId, s => s.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId)
           .Map(d => d.Description, s => s.ProjectOperationDetailContractorService.ProjectOperationDetail.Description)
           .Map(d => d.PrivateName, s => s.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PrivateName)
           .Map(d => d.PrivateCode, s => s.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PrivateCode)
           .Map(d => d.PublicCode, s => s.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicCode)
           .Map(d => d.PublicName, s => s.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicName)
           .Map(d => d.OperationInfoName, s => s.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName)
           .Map(d => d.OperationInfoCode, s => s.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode)
           .Map(d => d.ProjectOperationUnitOfMeasurementId, s => s.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId)
           .Map(d => d.FinalAmount, s => s.ProjectOperationDetailContractorService.ProjectOperationDetail.FinalAmount)
           .Map(d => d.ServiceInfoVolume, s => s.ProjectOperationDetailContractorService.Volume
           )
           ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        config.NewConfig<ContractorContractHeader, GetContractorContractHeaderByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ContractorId, s => s.ContractorId)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.CurrencyId, s => s.CurrencyId)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.FinalTotalAmount, s => s.FinalTotalAmount)
           .Map(d => d.TotalPercentageDoingJobWell, s => s.TotalPercentageDoingJobWell)
           .Map(d => d.TotalDoingJobWellAmount, s => s.TotalDoingJobWellAmount)
           .Map(d => d.TotalPercentageAdvancePayment, s => s.TotalPercentageAdvancePayment)
           .Map(d => d.TotalAdvancePaymentAmount, s => s.TotalAdvancePaymentAmount)
           .Map(d => d.TotalDailyLatenessPenalty, s => s.TotalDailyLatenessPenalty)
           .Map(d => d.TotalWorkDonePercent, s => s.TotalWorkDonePercent)
           .Map(d => d.TotalWorkDeliveryPercent, s => s.TotalWorkDeliveryPercent)
           .Map(d => d.TotalWorkCompletionPercent, s => s.TotalWorkCompletionPercent)
           .Map(d => d.Description, s => s.Description)
           ;

        config.NewConfig<ContractorContract, GetHeaderContractorContractModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ContractorContractTypeId, s => s.ContractorContractType)
           .Map(d => d.ContractorContractTypeName, s => s.ContractorContractType.GetEnumDescription())
           .Map(d => d.ContractorContractTypeCode, s => s.ContractorContractType)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.TotalAmount, s => s.TotalAmount)
           .Map(d => d.PercentageDoingJobWell, s => s.PercentageDoingJobWell)
           .Map(d => d.DoingJobWellAmount, s => s.DoingJobWellAmount)
           .Map(d => d.PercentageAdvancePayment, s => s.PercentageAdvancePayment)
           .Map(d => d.AdvancePaymentAmount, s => s.AdvancePaymentAmount)
           .Map(d => d.DailyLatenessPenalty, s => s.DailyLatenessPenalty)
           .Map(d => d.Description, s => s.Description);
        //.Map(d => d.Details, s => s.Details.Adapt<List<GetContractorContractHeaderByIdDetailModel>>())

        config.NewConfig<ContractorContract, GetFilteredContractorContractsModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ContractorContractTypeId, s => s.ContractorContractType)
           .Map(d => d.ContractorContractTypeName, s => s.ContractorContractType.GetEnumDescription())
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.TotalAmount, s => s.TotalAmount)
           .Map(d => d.PercentageDoingJobWell, s => s.PercentageDoingJobWell)
           .Map(d => d.DoingJobWellAmount, s => s.DoingJobWellAmount)
           .Map(d => d.PercentageAdvancePayment, s => s.PercentageAdvancePayment)
           .Map(d => d.AdvancePaymentAmount, s => s.AdvancePaymentAmount)
           .Map(d => d.DailyLatenessPenalty, s => s.DailyLatenessPenalty)
           .Map(d => d.WorkCompletionPercent, s => s.WorkCompletionPercent)
           .Map(d => d.WorkDeliveryPercent, s => s.WorkDeliveryPercent)
           .Map(d => d.WorkDonePercent, s => s.WorkDonePercent)
           .Map(d => d.Description, s => s.Description)
           ;

        config.NewConfig<ProjectOperationDetailContractorService, GetFilteredContractorContractHeaderInfoDetailModel>()
           .Map(d => d.OperationInfoCode, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode)
           .Map(d => d.OperationInfoName, s => s.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName)
           .Map(d => d.OperationWorkload, s => s.ProjectOperationDetail.ProjectOperation.Workload)
           .Map(d => d.ServiceInfoName, s => s.OperationInfoService.ServiceInfo.ServiceInfoName)
           .Map(d => d.ServiceInfoVolume, s => s.Volume)
            .Map(d => d.PrivateCode, s => s.ProjectOperationDetail.OperationLocation.PrivateName)
            .Map(d => d.PrivateName, s => s.ProjectOperationDetail.OperationLocation.PrivateCode)
            .Map(d => d.PublicName, s => s.ProjectOperationDetail.OperationLocation.PublicName)
            .Map(d => d.PublicCode, s => s.ProjectOperationDetail.OperationLocation.PublicCode)
           .Map(d => d.ProjectOperationId, s => s.ProjectOperationDetail.ProjectOperation.Id)
           .Map(d => d.ProjectOperationDetailServiceId, s => s.Id);

        config.NewConfig<ContractorContractHeaderHistory, GetContractorContractHistoryModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.FinalTotalAmount, s => s.FinalTotalAmount)
           .Map(d => d.TotalPercentageDoingJobWell, s => s.TotalPercentageDoingJobWell)
           .Map(d => d.TotalDoingJobWellAmount, s => s.TotalDoingJobWellAmount)
           .Map(d => d.TotalPercentageAdvancePayment, s => s.TotalPercentageAdvancePayment)
           .Map(d => d.TotalAdvancePaymentAmount, s => s.TotalAdvancePaymentAmount)
           .Map(d => d.TotalDailyLatenessPenalty, s => s.TotalDailyLatenessPenalty)
           .Map(d => d.TotalWorkDonePercent, s => s.TotalWorkDonePercent)
           .Map(d => d.TotalWorkDeliveryPercent, s => s.TotalWorkDeliveryPercent)
           .Map(d => d.TotalWorkCompletionPercent, s => s.TotalWorkCompletionPercent)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.CreatorId, s => s.CreatorId)
           .Map(d => d.Created, s => s.Created)
           ;
    }
}
