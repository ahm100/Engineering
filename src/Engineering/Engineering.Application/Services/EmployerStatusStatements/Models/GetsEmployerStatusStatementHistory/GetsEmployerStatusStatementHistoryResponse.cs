
namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementHistory;

public record GetsEmployerStatusStatementHistoryResponse(
    List<GetsEmployerStatusStatementHistoryResponseModel> Data,
    int RowCount
    );

public record GetsEmployerStatusStatementHistoryResponseModel
{
    public long Id { get; set; }
    public Domain.Entities.EmployerStatusStatements.Enums.EmployerStatusStatementStatus Status { get; set; }
    public string StatusDescription { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string Created { get; set; } = string.Empty;
    public string StatusStatementCode { get; set; } = string.Empty;
    public decimal PercentageOfWorkDone { get; set; }
    public decimal CalculatedAmount { get; set; }
    public string? Description { get; set; }
}
