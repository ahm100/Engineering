namespace Engineering.Application.Services.ProjectOperations.Models.GetsProposedPrice;

public record GetsProposedPriceModel(
    long Id,
    long OperationInfoId,
    string OperationInfoName,
    string OperationInfoCode,
    long? EmployerContractId,
    string? ContractCode,
    long EmployerId,
    string? EmployerName,
    DateTime? StartDate,
    DateTime? EndDate,
    long ProjectId,
    string ProjectName,
    long CostCenterId,
    string CostCenterName,
    decimal Workload,
    decimal TolerancePercentage,
    decimal? Price,
    long? CurrencyUnitId,
    string? CurrencyUnitName,
    long? CompanyId,
    string? CompanyNameFa
    );
