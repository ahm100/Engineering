using Engineering.Application.Services.Contracts.Contracts.ContractChanges;
using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractChange;

public record UpdateContractChangeRequest(
    long ContractId,
    long Id,
    ContractChangeType Type,
    string Number,
    DateTime Date,
    string Subject,
    int? DurationChange,
    List<string> Urls,
    List<ContractChangeItemRequest>? Items) : IHttpRequest;
