using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementStatusChanger;

public record ContractorStatusStatementStatusChangerRequest(
    long Id,
    CSSStatus Status,
    long? SeasonId,
    DateTime? ConfirmedPaymentDate,
    long? ConfirmedBankAccountId,
    decimal? ConfirmedPrice,
    List<StatusStatementDailyService>? DailyServices,
    List<FixedContractPctModel>? FixedContractPct,
    List<string>? Urls,
    List<string>? SelectedUrls,
    string? Description,
    bool? MultiPayment,
    long? CostCategoryId,
    long? CostGroupId,
    long? DocumentTypeId,
    long? PreferentialTypeId
    ) : IHttpRequest;

public record StatusStatementDailyService(
    long Id,
    decimal ManagementApprovalPercentage,
    string? ApprovedDescription
    );

public record ManagerDataModel(
    long? UserId,
    string? FullName,
    string? StatusDescription,
    string? Description
    );

public record FixedContractPctModel(
    long Id,
    decimal ApprovalPercentage,
    string? ApprovedDescription
    );
