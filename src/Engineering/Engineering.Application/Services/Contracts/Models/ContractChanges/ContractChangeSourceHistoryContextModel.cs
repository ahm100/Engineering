namespace Engineering.Application.Services.Contracts.Models.ContractChanges;

public record ContractChangeSourceHistoryContextModel(
    long ContractTypeId,
    long ProjectOperationDetailId,
    decimal? PriorNewValue,
    decimal? CurrentNewValue,
    long? PriorUnitOfMeasurementId,
    decimal? PriorUnitPrice);
