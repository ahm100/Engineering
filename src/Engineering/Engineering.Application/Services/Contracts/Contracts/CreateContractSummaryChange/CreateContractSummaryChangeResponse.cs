namespace Engineering.Application.Services.Contracts.Contracts.CreateContractSummaryChange;

public record CreateContractSummaryChangeResponse(
    long Id,
    decimal NewContractAmount,
    DateTime NewEndDate,
    bool IsDone);
