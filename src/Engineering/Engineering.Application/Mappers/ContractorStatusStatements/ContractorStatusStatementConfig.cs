using Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementById;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetFilteredContractorStatusStatement;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementDiscountById;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Mappers.ContractorStatusStatements;

public class ContractorStatusStatementConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<ContractorStatusStatement, GetContractorStatusStatementByIdResponse>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.CategoryId, s => s.Season.Branch.Category.Id)
           .Map(d => d.CategoryName, s => s.Season.Branch.Category.CategoryName)
           .Map(d => d.BranchId, s => s.Season.Branch.Id)
           .Map(d => d.BranchName, s => s.Season.Branch.BranchName)
           .Map(d => d.SeasonId, s => s.Season.Id)
           .Map(d => d.SeasonName, s => s.Season.SeasonName)
           .Map(d => d.ProjectId, s => s.Project.Id)
           .Map(d => d.Project, s => s.Project.ProjectName)
           .Map(d => d.ContractorId, s => s.ContractorId)
           .Map(d => d.Code, s => s.Code)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.DiscountedAmount, s => s.ContractorStatusStatementDiscounts.Sum(x => x.DiscountPrice))
           .Map(d => d.FinalTotalAmount, s => s.FinalTotalAmount - s.ContractorStatusStatementDiscounts.Sum(x => x.DiscountPrice))
           .Map(d => d.TotalPercentageDoingJobWell, s => s.TotalPercentageDoingJobWell)
           .Map(d => d.TotalDoingJobWellAmount, s => s.TotalDoingJobWellAmount)
           .Map(d => d.TotalAdvancePaymentAmount, s => s.TotalAdvancePaymentAmount)
           .Map(d => d.TotalPercentageAdvancePayment, s => s.TotalPercentageAdvancePayment)
           .Map(d => d.TotalDailyLatenessPenalty, s => s.TotalDailyLatenessPenalty)
           .Map(d => d.TotalWorkDonePercent, s => s.TotalWorkDonePercent)
           .Map(d => d.TotalWorkDeliveryPercent, s => s.TotalWorkDeliveryPercent)
           .Map(d => d.TotalWorkCompletionPercent, s => s.TotalWorkCompletionPercent)
           .Map(d => d.ProductsAmount, s => s.ProductsAmount)
           .Map(d => d.FinesAmount, s => s.FinesAmount)
           .Map(d => d.RewardsAmount, s => s.RewardsAmount)
           .Map(d => d.CostOversAmount, s => s.CostOversAmount)
           .Map(d => d.ThirdPartiesAmount, s => s.ThirdPartiesAmount)
           .Map(d => d.Paymented, s => s.PaymentedAmount)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.IsLast, s => s.IsLast)
           .Map(d => d.ManagmentDescription, s => s.ManagmentDescription)
           ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<ContractorStatusStatement, GetFilteredContractorStatusStatementModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.CostCenterId,
           s => s.Project.ProjectCostCenters
           .Select(x => (long?)x.CostCenterId)
           .FirstOrDefault())
           .Map(d => d.CostCenterName,
           s => s.Project.ProjectCostCenters
           .Select(x => x.CostCenter.CostCenterName)
           .FirstOrDefault())
           .Map(d => d.ProjectId, s => s.Project.Id)
           .Map(d => d.ProjectManagerId, s => s.Project.ProjectManager)
           .Map(d => d.Project, s => s.Project.ProjectName)
           .Map(d => d.ContractorId, s => s.ContractorId)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.CreatorId, s => s.CreatorId)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.FinalTotalAmount, s => s.FinalTotalAmount)
           .Map(d => d.TotalPercentageDoingJobWell, s => s.TotalPercentageDoingJobWell)
           .Map(d => d.TotalDoingJobWellAmount, s => s.TotalDoingJobWellAmount)
           .Map(d => d.TotalAdvancePaymentAmount, s => s.TotalAdvancePaymentAmount)
           .Map(d => d.TotalPercentageAdvancePayment, s => s.TotalPercentageAdvancePayment)
           .Map(d => d.TotalDailyLatenessPenalty, s => s.TotalDailyLatenessPenalty)
           .Map(d => d.TotalWorkDonePercent, s => s.TotalWorkDonePercent)
           .Map(d => d.TotalWorkDeliveryPercent, s => s.TotalWorkDeliveryPercent)
           .Map(d => d.TotalWorkCompletionPercent, s => s.TotalWorkCompletionPercent)
           .Map(d => d.ProductsAmount, s => s.ProductsAmount)
           .Map(d => d.FinesAmount, s => s.FinesAmount)
           .Map(d => d.RewardsAmount, s => s.RewardsAmount)
           .Map(d => d.ThirdPartiesAmount, s => s.ThirdPartiesAmount)
           .Map(d => d.PaymentedAmount, s => s.PaymentedAmount)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.IsLast, s => s.IsLast)
           .Map(d => d.ManagmentDescription, s => s.ManagmentDescription)
           ;

        config.NewConfig<ContractorStatusStatement, PaidContractorStatusStatementModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.StartDate, s => s.StartDate)
           .Map(d => d.EndDate, s => s.EndDate)
           .Map(d => d.CreatorId, s => s.CreatorId)
           .Map(d => d.Status, s => s.Status)
           .Map(d => d.FinalTotalAmount, s => s.FinalTotalAmount)
           .Map(d => d.TotalPercentageDoingJobWell, s => s.TotalPercentageDoingJobWell)
           .Map(d => d.TotalDoingJobWellAmount, s => s.TotalDoingJobWellAmount)
           .Map(d => d.TotalAdvancePaymentAmount, s => s.TotalAdvancePaymentAmount)
           .Map(d => d.TotalPercentageAdvancePayment, s => s.TotalPercentageAdvancePayment)
           .Map(d => d.TotalDailyLatenessPenalty, s => s.TotalDailyLatenessPenalty)
           .Map(d => d.TotalWorkDonePercent, s => s.TotalWorkDonePercent)
           .Map(d => d.TotalWorkDeliveryPercent, s => s.TotalWorkDeliveryPercent)
           .Map(d => d.TotalWorkCompletionPercent, s => s.TotalWorkCompletionPercent)
           .Map(d => d.ProductsAmount, s => s.ProductsAmount)
           .Map(d => d.FinesAmount, s => s.FinesAmount)
           .Map(d => d.RewardsAmount, s => s.RewardsAmount)
           .Map(d => d.ThirdPartiesAmount, s => s.ThirdPartiesAmount)
           .Map(d => d.PaymentedAmount, s => s.PaymentedAmount)
           .Map(d => d.ConfirmedPrice, s => s.ConfirmedPrice)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.IsLast, s => s.IsLast)
           .Map(d => d.ManagmentDescription, s => s.ManagmentDescription)
           ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        config.NewConfig<ContractorStatusStatementDetail, GetContractorStatusStatementDetailModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ContractorStatusStatementId, s => s.ContractorStatusStatement.Id)
           .Map(d => d.ContractorContractId, s => s.ContractorContract.Id)
           .Map(d => d.ContractorContractHeaderId, s => s.ContractorContract.ContractorContractHeader.Id)
           .Map(d => d.ContractorContractType, s => s.ContractorContract.ContractorContractType.GetEnumDescription())
           .Map(d => d.ContractorContractTypeCode, s => s.ContractorContract.ContractorContractType)
           .Map(d => d.StartDate, s => TimeCalculator.DatePiker(s.StartDate))
           .Map(d => d.EndDate, s => TimeCalculator.DatePiker(s.EndDate))
           .Map(d => d.TotalAmount, s => s.TotalAmount)
           .Map(d => d.PercentageDoingJobWell, s => s.PercentageDoingJobWell)
           .Map(d => d.DoingJobWellAmount, s => s.DoingJobWellAmount)
           .Map(d => d.PercentageAdvancePayment, s => s.PercentageAdvancePayment)
           .Map(d => d.AdvancePaymentAmount, s => s.AdvancePaymentAmount)
           .Map(d => d.DailyLatenessPenalty, s => s.DailyLatenessPenalty)
           .Map(d => d.WorkDonePercent, s => s.WorkDonePercent)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.WorkDonePercent, s => s.WorkDonePercent)
           .Map(d => d.WorkDeliveryPercent, s => s.WorkDeliveryPercent)
           .Map(d => d.WorkCompletionPercent, s => s.WorkCompletionPercent)
           ;


        config.NewConfig<ContractorStatusStatementDiscount, GetsContractorStatusStatementDiscountByIdModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.DiscountPrice, s => s.DiscountPrice)
           .Map(d => d.Description, s => s.Description)
           .Map(d => d.RegistrationDate, s => TimeCalculator.DatePiker(s.RegistrationDate))
           ;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        config.NewConfig<ContractorStatusStatementService, GetContractorStatusStatementByIdServiceModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ContractorContractDetailId, s => s.ContractorContractDetail.Id)
           .Map(d => d.DailyProjectOperationId, s => s.DailyProjectOperation.Id)
           //.Map(d => d.ServiceInfoId, s => s.ContractorContractDetail.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.Id)
           //.Map(d => d.ServiceInfoName, s => s.ContractorContractDetail.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.ServiceInfoName)
           //.Map(d => d.ServiceInfoCode, s => s.ContractorContractDetail.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.ServiceInfoCode)
           //.Map(d => d.UnitOfMeasurementId, s => s.ContractorContractDetail.ProjectOperationDetailContractorService!.OperationInfoService.ServiceInfo.UnitOfMeasurementId)
           .Map(d => d.ThirdPartiesAmount, s => s.ThirdPartiesAmount)
           ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.


        config.NewConfig<ContractorStatusStatementServiceDaily, GetContractorStatusStatementServiceDailiesModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.DailyProjectOperationServiceId, s => s.DailyProjectOperationService.Id)
           .Map(d => d.Volume, s => s.Volume)
           .Map(d => d.DailyDate, s => s.DailyProjectOperationService.DailyProjectOperation.StartDate)
           .Map(d => d.TimeSpant, s => TimeCalculator.TicksToStringHM((long)s.TimeSpant!))
           .Map(d => d.Price, s => s.TotalPrice)
           .Map(d => d.AcceptablePercentage, s => s.AcceptablePercentage)
           .Map(d => d.AcceptableAmount, s => s.AcceptableAmount)
           .Map(d => d.AcceptableDescription, s => s.AcceptableDescription)
           .Map(d => d.ProjectManagementApprovalPercentage, s => s.ProjectManagementApprovalPercentage)
           .Map(d => d.ProjectManagementApprovedPrice, s => s.ProjectManagementApprovedPrice)
           .Map(d => d.ProjectManagementApprovedDescription, s => s.ProjectManagementApprovedDescription)
           .Map(d => d.ManagementApprovalPercentage, s => s.ManagementApprovalPercentage)
           .Map(d => d.ApprovedPrice, s => s.ApprovedPrice)
           .Map(d => d.ApprovedDescription, s => s.ApprovedDescription)
           ;

        config.NewConfig<ContractorStatusStatementServiceThirdParty, GetContractorStatusStatementByIdServiceThirdPartiesModel>()
           .Map(d => d.Id, s => s.Id)
           .Map(d => d.ContractorStatusStatementServiceId, s => s.ContractorStatusStatementService.Id)
           .Map(d => d.ThirdPartyId, s => s.ThirdPartyId)
           .Map(d => d.SkillId, s => s.SkillId)
           .Map(d => d.Type, s => s.Type)
           .Map(d => d.WorkingDay, s => TimeCalculator.DatePiker(s.WorkingDay))
           .Map(d => d.Price, s => s.Price)
           ;

    }
}
