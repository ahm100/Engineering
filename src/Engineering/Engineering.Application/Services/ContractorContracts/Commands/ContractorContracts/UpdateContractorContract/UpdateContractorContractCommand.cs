using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.UpdateContractorContract;

public record UpdateContractorContractCommand(
    ContractorContract Entity,
    DateTime StartDate,
    DateTime EndDate,
    decimal? TotalAmount,
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
