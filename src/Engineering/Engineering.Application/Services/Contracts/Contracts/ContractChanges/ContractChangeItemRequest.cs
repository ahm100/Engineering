namespace Engineering.Application.Services.Contracts.Contracts.ContractChanges;

public record ContractChangeItemRequest(
    long? ContractTypeDetailId,
    ContractChangeSourceItemRequest? SourceItem,
    decimal NewValue);
