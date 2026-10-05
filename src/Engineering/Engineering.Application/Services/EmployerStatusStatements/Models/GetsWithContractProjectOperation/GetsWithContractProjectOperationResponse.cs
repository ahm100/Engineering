
namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsWithContractProjectOperation;

public record GetsWithContractProjectOperationResponse(
    List<GetsWithContractProjectOperationResponseModel> Data,
    int RowCount
    );

public record GetsWithContractProjectOperationResponseModel
{
    public long Id { get; set; }
    public Domain.Entities.EmployerStatusStatements.Enums.EmployerStatusStatementStatus SendStatusType { get; set; }
    public string SendStatusTypeTitle { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
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
}