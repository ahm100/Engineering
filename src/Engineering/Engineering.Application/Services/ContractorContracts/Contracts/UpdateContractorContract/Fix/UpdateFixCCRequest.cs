using Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;

namespace Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract;

public record UpdateFixCCRequest(
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
    List<FixContractNewDetatilServiceModel>? NewDetailServiceModels,
    List<CreateContractorContractDetailCostOverModel>? CreateCostOvers,
    List<UpdateContractorContractDetailCostOverModel>? UpdateCostOvers,
    List<long>? DeleteContractorContractDetailCostOverIds,
    List<long>? DeleteContractorContractDetailServiceIds,
    List<long>? DeleteContractorContractDetailIds,
    bool IsDelete
    ) : IHttpRequest;

public record FixContractNewDetatilServiceModel(
    List<long>? ServiceIds,
    List<long>? ProjectServiceIds,
    List<long>? ProjectOperationDetailServiceIds,
    List<CreateContractorContractDetailCostOverModel>? CreateCostOvers
    );
