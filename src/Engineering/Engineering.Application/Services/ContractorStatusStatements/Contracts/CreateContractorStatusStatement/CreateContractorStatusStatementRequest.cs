
namespace Engineering.Application.Services.ContractorStatusStatements.Models.CreateContractorStatusStatement;

public record CreateContractorStatusStatementRequest(
    long ContractorId,
    long ProjectId,
    string? Code,
    decimal? CreatorConfirmedAmount,
    string? Description,
    bool AccsseToInvalidated,
    string? InvalidatedDescription,
    DateTime StartDate,
    DateTime EndDate,
    List<string>? Urls,
    List<PercentageOfFixContractModel>? FixContracts,
    List<ContractorContractDailyService>? DailyServices,
    List<ContractorStatusStatementDiscountModel>? Discounts
    ) : IHttpRequest;

public record PercentageOfFixContractModel(
    long Id,
    decimal? FixedContractPct,
    string? Description
    );

public record ContractorStatusStatementDiscountModel(
    long RequestRewardId,
    decimal DiscountPrice,
    DateTime? RegistrationDate,
    string? Description
    );

public record ContractorContractDailyService(
    long DailyServiceId,
    decimal AcceptablePercentage,
    string? AcceptableDescription
    );