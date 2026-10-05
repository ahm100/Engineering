
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperation;

public record GetsEmployerStatusStatementProjectOperationResponse(
    List<GetsEmployerStatusStatementProjectOperationResponseModel> Data,
    int RowCount
    );

public record GetsEmployerStatusStatementProjectOperationResponseModel
{
    public long Id { get; set; }
    public long ProjectOperationId { get; set; }
    public ProjectOperationStatus ProjectOperationStatus { get; set; }
    public string ProjectOperationStatusTitle { get; set; } = string.Empty;
    public long OperationInfoId { get; set; }
    public string? OperationInfoName { get; set; }
    public string? OperationInfoCode { get; set; }
    public decimal TotalWorkVolume { get; set; }
    public decimal DoneWorkVolume { get; set; }
    public decimal StandardDeviation { get; set; }
    public decimal StatusStatementWorkVolume { get; set; }
    public long UnitOfMeasurementId { get; set; }
    public string? UnitOfMeasurementName { get; set; }
    public decimal DonePercentage { get; set; }
    public decimal TotalPercentage { get; set; }
    public decimal CalculatedAmount { get; set; }
    public decimal TotalDetailWorkVolume { get; set; }
    public decimal TotalDailyWorkVolume { get; set; }
    public decimal ContractorWorkVolume { get; set; }
    public decimal SupervisorWorkVolume { get; set; }
    public decimal ConsultantWorkVolume { get; set; }
    public decimal EmployerRepresentativeWorkVolume { get; set; }
    public decimal ContractorUnitPrice { get; set; }
    public decimal ContractorTotalPrice { get; set; }
    public decimal ContractorConfirmeTotalPrice { get; set; }
    public decimal EmployerCommercialUnitPrice { get; set; }
    public decimal EmployerCommercialTotalPrice { get; set; }
    public decimal EmployerCommercialConfirmeTotalPrice { get; set; }
    public string? Description { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}
