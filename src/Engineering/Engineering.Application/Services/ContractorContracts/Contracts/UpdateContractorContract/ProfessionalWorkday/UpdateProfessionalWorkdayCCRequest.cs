namespace Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract;

public record UpdateProfessionalWorkdayCCRequest(
    long Id,
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalAmount,
    decimal? PercentageDoingJobWell,
    decimal? DoingJobWellAmount,
    decimal? PercentageAdvancePayment,
    decimal? AdvancePaymentAmount,
    decimal? DailyLatenessPenalty,
    decimal? DailyBaseHours,
    decimal? MonthlyBaseHours,
    string? Description,
    List<UpdatePersonContractorContractDetailModel> Details,
    bool IsDelete
    );

public record UpdatePersonContractorContractDetailModel(
    long? ContractorContractDetailId,
    long ServiceId,
    bool IsDelete
    );