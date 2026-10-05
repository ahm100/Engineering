namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record SetContractorStatementToProjectManagerConfirmedRequest(
    long Id,
    string? Description,
    decimal? ProjectManagerConfirmedAmount,
    List<string>? SelectedUrls,
    List<StatusStatementDailyService>? DailyServices,
    List<FixedContractPctModel>? FixedContractPcts
    ) : IHttpRequest;
