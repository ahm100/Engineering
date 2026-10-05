using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementExcelExporter;

public record GetEmployerStatusStatementExcelExporterResponseModel
{
    public long Id { get; set; }
    public Domain.Entities.EmployerStatusStatements.Enums.EmployerStatusStatementStatus SendStatusType { get; set; }
    public string SendStatusTypeTitle { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string Created { get; set; } = string.Empty;
    public string StatusStatementCode { get; set; } = string.Empty;
    public long EmployerId { get; set; }
    public string? EmployerName { get; set; }
    public long EmployerContractId { get; set; }
    public string? EmployerContractCode { get; set; }
    public long CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public decimal PercentageOfWorkDone { get; set; }
    public decimal CalculatedAmount { get; set; }
    public long CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}

public record EmployerStatusStatementProjectOperations
{
    public long Id { get; set; }
    public long ProjectOperationId { get; set; }
    public ProjectOperationStatus ProjectOperationStatus { get; set; }
    public string ProjectOperationStatusTitle { get; set; } = string.Empty;
    public long OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; }
    public string? OperationInfoCode { get; set; }
    public decimal StandardDeviation { get; set; }
    public decimal TotalWorkVolume { get; set; }
    public decimal DoneWorkVolume { get; set; }
    public decimal StatusStatementWorkVolume { get; set; }
    public long UnitOfMeasurementId { get; set; }
    public string? UnitOfMeasurementName { get; set; }
    public decimal DonePercentage { get; set; }
    public decimal TotalPercentage { get; set; }
    public decimal CalculatedAmount { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}

public record EmployerStatusStatementProjectOperationDetails
{
    public long Id { get; set; }
    public long ProjectOperationId { get; set; }
    public long ProjectOperationDetailId { get; set; }
    public ProjectOperationDetailStatus ProjectOperationDetailStatus { get; set; }
    public string ProjectOperationDetailStatusTitle { get; set; } = string.Empty;
    public long OperationLocationId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public decimal TotalWorkVolume { get; set; }
    public decimal DoneWorkVolume { get; set; }
    public decimal StatusStatementWorkVolume { get; set; }
    public decimal DonePercentage { get; set; }
    public decimal TotalPercentage { get; set; }
    public decimal CalculatedAmount { get; set; }
}
