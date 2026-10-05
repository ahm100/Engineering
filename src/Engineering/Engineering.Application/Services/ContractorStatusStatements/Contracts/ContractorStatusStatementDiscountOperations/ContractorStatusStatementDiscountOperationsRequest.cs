namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementDiscountOperations;

public record ContractorStatusStatementDiscountOperationsRequest(
    long ContractorStatusStatementId,
    List<ContractorStatusStatementDiscountOperationsModel> ContractorStatusStatementDiscounts
    ) : IHttpRequest;

public record ContractorStatusStatementDiscountOperationsModel(
    long? Id,
    decimal? DiscountPrice,
    long? RequestRewardId,
    DateTime? RegistrationDate,
    string? Description,
    bool IsDeleted
    );