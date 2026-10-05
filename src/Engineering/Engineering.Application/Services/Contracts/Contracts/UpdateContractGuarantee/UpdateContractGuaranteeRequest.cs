using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractGuarantee;

public record UpdateContractGuaranteeRequest(
    long ContractId,
    long Id,
    ContractGuaranteeType Type,
    decimal Amount,
    decimal? Percentage,
    string Number,
    DateTime IssueDate,
    DateTime ExpiryDate,
    string? FileUrl) : IHttpRequest;
