using Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;

namespace Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract;

public record UpdateServiceCCRequest(
    long Id,
    DateTime StartDate,
    DateTime EndDate,
    decimal TotalAmount,
    decimal? PercentageDoingJobWell,
    decimal? DoingJobWellAmount,
    decimal? PercentageAdvancePayment,
    decimal? AdvancePaymentAmount,
    decimal? DailyLatenessPenalty,
    string? Description,
    List<UpdateServiceContractorContractModel> UpdateDetails,
    List<CreateServiceContractorContractModel> CreateDetails,
    bool IsDelete
    );

public record UpdateServiceContractorContractModel(
    long ContractorContractDetailId,
    DateTime? StartDate,
    DateTime? EndDate,
    decimal? UnitAmount,
    decimal? TotalAmount,
    List<UpdateServiceContractorContractPriceModel>? UpdatePrices,
    List<CreateServiceContractorContractPriceModel>? CreatePrices,
    List<CreateContractorContractDetailCostOverModel>? CreateCostOvers,
    List<UpdateContractorContractDetailCostOverModel>? UpdateCostOvers,
    List<long>? DeleteContractorContractDetailServiceIds,
    List<long>? DeleteContractorContractDetailPriceIds,
    List<long>? DeleteContractorContractDetailCostOverIds,
    bool IsDelete
    );

public record UpdateServiceContractorContractPriceModel(
    long ContractorContractDetailPriceId,
    DateTime StartDate,
    DateTime EndDate,
    decimal Price,
    bool IsActive
    );

public record UpdateContractorContractDetailCostOverModel(
    long ContractorContractDetailCostOverId,
    long CostOverId,
    long ContractorId,
    decimal Percentage,
    string? Description
    );