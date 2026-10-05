
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsForEmployerStatusStatement;

public record GetsForEmployerStatusStatementResponse(
    List<GetsForEmployerStatusStatementModel> Data,
    int RowCount);

public record GetsForEmployerStatusStatementModel
{
    public long Id { get; set; }
    public ProjectOperationStatus ProjectOperationStatus { get; set; }
    public string ProjectOperationStatusTitle => ProjectOperationStatus.GetEnumDescription();
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public decimal TotalWorkVolume { get; set; }
    public decimal DoneWorkVolume { get; set; }
    public decimal StatusStatementWorkVolume { get; set; }
    public decimal TotalDetailWorkVolume { get; set; }
    public decimal TotalDailyWorkVolume { get; set; }
    public decimal StandardDeviation => ((TotalWorkVolume / DoneWorkVolume) * 100) - 100;
    public decimal? PriceUnit { get; set; } = 0;
    public long UnitOfMeasurementId { get; set; }
    public string? UnitOfMeasurementName { get; set; }
    public decimal DonePercentage => Math.Round(((DoneWorkVolume / TotalWorkVolume) * 100), 3);
    public decimal TolerancePercentage { get; set; }
    public bool HaveContract { get; set; }
    public int? Priority { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? LastStatusStatement { get; set; }
    public DateTime Created { get; set; }
    public string? Description { get; set; } = string.Empty;
};