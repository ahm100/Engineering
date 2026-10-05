namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToManagementConfirmedRequest(
    long Id,
    string? Description,
    bool? MultiPayment,
    decimal? ManagementConfirmedAmount,
    List<string>? SelectedUrls,
    List<StatusStatementDailyService>? DailyServices,
    List<FixedContractPctModel>? FixedContractPcts
    ) : IHttpRequest;
