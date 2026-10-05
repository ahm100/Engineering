using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.CreateContractorContract;

public record CreateContractorContractCommand(
    long CompanyId,
    ContractorContractHeader ContractorContractHeader,
    ContractorContractType ContractorContractType,
    Project Project,
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalAmount,
    decimal? PercentageDoingJobWell,
    decimal? DoingJobWellAmount,
    decimal? PercentageAdvancePayment,
    decimal? AdvancePaymentAmount,
    decimal? DailyLatenessPenalty,
    int? WorkDonePercent,
    int? WorkDeliveryPercent,
    int? WorkCompletionPercent,
    decimal? DailyBaseHours,
    decimal? MonthlyBaseHours,
    string? Description
    ) : ICommand<ContractorContract>;
