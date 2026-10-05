
namespace Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;

public record CreateItemPriceListCCRequest(
    DateTime StartDate,
    DateTime EndDate,
    decimal? PercentageDoingJobWell,
    decimal? DoingJobWellAmount,
    decimal? PercentageAdvancePayment,
    decimal? AdvancePaymentAmount,
    decimal? DailyLatenessPenalty,
    string? Description,
    List<CreatePoCCModel> Details
)
{
    public List<long>? ProjectOperationDetailServiceIds { get; init; }
}

public record CreatePoCCModel(
    long ProjectOperationId,
    decimal ContractCoefficient
    );
