namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToPaymentConfirmationRequest(
    long Id,
    long? SeasonId,
    DateTime? ConfirmedPaymentDate,
    long? ConfirmedBankAccountId,
    decimal? ConfirmedPrice,
    List<string>? Urls,
    List<string>? SelectedUrls,
    string? Description,
    long? CostCategoryId,
    long? CostGroupId,
    long? DocumentTypeId,
    long? PreferentialTypeId
    ) : IHttpRequest;
