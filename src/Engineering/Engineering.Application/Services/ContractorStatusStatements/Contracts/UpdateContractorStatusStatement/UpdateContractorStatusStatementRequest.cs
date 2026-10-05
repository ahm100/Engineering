namespace Engineering.Application.Services.ContractorStatusStatements.Models.UpdateContractorStatusStatement;

public record UpdateContractorStatusStatementRequest(
    long Id,
    decimal? UpdatorConfirmedAmount,
    decimal? PaymentedAmount,
    string? Description,
    bool AccsseToInvalidated,
    string? InvalidatedDescription,
    List<string>? Urls,
    List<UpdatePercentageOfFixContractModel>? FixContracts,
    List<UpdateStatusStatementDailyService>? DailyServices,
    List<UpdateStatusStatementDiscountModel>? Discounts
    ) : IHttpRequest;

public record UpdatePercentageOfFixContractModel(
    long Id,
    decimal? FixedContractPct,
    string? FixedContractPctDesc
    );

public record UpdateStatusStatementDailyService(
    long Id,
    decimal AcceptablePercentage,
    string? AcceptableDescription
    );

public record UpdateStatusStatementDiscountModel(
    long Id,
    long RequestRewardId,
    decimal DiscountPrice,
    DateTime? RegistrationDate,
    string? Description
    );