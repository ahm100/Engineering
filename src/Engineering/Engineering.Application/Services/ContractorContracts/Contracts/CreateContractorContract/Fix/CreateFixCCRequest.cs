
namespace Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;

public record CreateFixCCRequest(
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalAmount,
    decimal? PercentageDoingJobWell,
    decimal? DoingJobWellAmount,
    decimal? PercentageAdvancePayment,
    decimal? AdvancePaymentAmount,
    decimal? DailyLatenessPenalty,
    List<long>? ServiceIds,
    List<long>? ProjectServiceIds,
    List<long>? ProjectOperationDetailServiceIds,
    List<CreateContractorContractDetailCostOverModel>? CreateCostOvers,
    string? Description
    );
