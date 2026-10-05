namespace Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;

public record CreateServiceCCRequest(
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalAmount,
    decimal? PercentageDoingJobWell,
    decimal? DoingJobWellAmount,
    decimal? PercentageAdvancePayment,
    decimal? AdvancePaymentAmount,
    decimal? DailyLatenessPenalty,
    string? Description,
    List<CreateServiceContractorContractModel> Details
    );

public record CreateServiceContractorContractModel(
    long? ServiceId,
    long? ProjectServiceId,
    List<long>? ProjectOperationDetailServiceIds,
    DateTime? StartDate,
    DateTime? EndDate,
    List<CreateServiceContractorContractPriceModel> Prices,
    List<CreateContractorContractDetailCostOverModel>? CreateCostOvers
    );

public record CreateServiceContractorContractPriceModel(
    DateTime StartDate,
    DateTime EndDate,
    decimal Price,
    bool IsActive
    );

public record CreateContractorContractDetailCostOverModel(
    long CostOverId,
    long ContractorId,
    decimal Percentage,
    string? Description
    );