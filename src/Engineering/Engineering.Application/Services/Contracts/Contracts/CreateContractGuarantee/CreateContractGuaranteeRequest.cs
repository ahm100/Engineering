using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.CreateContractGuarantee;

public record CreateContractGuaranteeRequest(
    long ContractId,
    ContractGuaranteeType Type,
    decimal Amount,
    decimal? Percentage,
    string Number,
    DateTime IssueDate,
    DateTime ExpiryDate,
    string? FileUrl) : IHttpRequest;
