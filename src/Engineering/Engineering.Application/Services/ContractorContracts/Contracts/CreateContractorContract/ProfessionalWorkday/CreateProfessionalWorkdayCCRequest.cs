namespace Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;

public record CreateProfessionalWorkdayCCRequest(
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalAmount,
    decimal? PercentageDoingJobWell,
    decimal? DoingJobWellAmount,
    decimal? PercentageAdvancePayment,
    decimal? AdvancePaymentAmount,
    decimal? DailyLatenessPenalty,
    List<long>? ProjectOperationDetailServiceIds,
    decimal? DailyBaseHours,
    decimal? MonthlyBaseHours,
    string? Description
    );