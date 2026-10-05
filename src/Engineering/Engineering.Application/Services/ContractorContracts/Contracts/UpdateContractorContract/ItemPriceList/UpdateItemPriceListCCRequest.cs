using Engineering.Application.Services.ContractorContracts.Contracts.CreateContractorContract;

namespace Engineering.Application.Services.ContractorContracts.Contracts.UpdateContractorContract.ItemPriceList;

public record UpdateItemPriceListCCRequest(
    long Id,
    DateTime StartDate,
    DateTime EndDate,
    decimal? PercentageDoingJobWell,
    decimal? DoingJobWellAmount,
    decimal? PercentageAdvancePayment,
    decimal? AdvancePaymentAmount,
    decimal? DailyLatenessPenalty,
    string? Description,
    List<CreatePoCCModel>? Details,
    List<UpdatePoCCModel>? UpdateDetails,
    List<long>? DeleteDetails,
    bool IsDelete
)
{
    /// <summary>
    /// OperationBased assignment/PODCS ids used only for new Details.
    /// </summary>
    public List<long>? ProjectOperationDetailServiceIds { get; init; }
}

public record UpdatePoCCModel(
    long Id,
    long ProjectOperationId,
    decimal ContractCoefficient
    );