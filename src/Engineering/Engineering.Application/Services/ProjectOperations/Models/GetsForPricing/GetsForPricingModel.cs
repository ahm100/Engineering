namespace Engineering.Application.Services.ProjectOperations.Models.GetsForPricing;

public record GetsForPricingModel(
    long Id,
    long OperationInfoId,
    string OperationInfoName,
    string OperationInfoCode,
    decimal Workload,
    decimal TolerancePercentage,
    decimal? Price,
    long UnitOfMeasurementId,
    string? MeasurementName,
    long? CurrencyUnitId,
    string? CurrencyUnitName,
    bool HaveContract,
    long? CompanyId,
    string? CompanyNameFa
    );
