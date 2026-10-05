namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractSummaryChange;

public record UpdateContractSummaryChangeResponse(
    decimal NewContractAmount,
    DateTime NewEndDate,
    bool IsDone);
