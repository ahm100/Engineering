namespace Engineering.Application.Services.Contracts.Models.ContractChanges;

public record ContractChangeMutationContextModel(
    long? LatestChangeId,
    DateTime? LatestChangeDate,
    bool TargetExists,
    DateTime? PreviousChangeDate,
    decimal TargetFinancialChangeAmount,
    int? TargetDurationChange,
    Engineering.Domain.Entities.Contracts.Enums.ContractChangeMode? TargetMode);
