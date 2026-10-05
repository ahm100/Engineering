namespace Engineering.Application.Services.Dashboard.Contracts.GetProjectDashboard;

public class GetProjectDashboardResponse
{
    public int? DelayedPOS { get; set; }
    public int? NearEndPOS { get; set; }
    public DateTime? EstimatedFinishDate { get; set; }
    public string? EstimatedFinishDateShamsi => EstimatedFinishDate.ToShamsi();
    public int? DelayDays => EstimatedFinishDate.HasValue && PlannedFinishDate.HasValue ?
        (EstimatedFinishDate.Value - PlannedFinishDate.Value).Days : 0;
    public DateTime? PlannedFinishDate { get; set; }
    public string? PlannedFinishDateShamsi => PlannedFinishDate.ToShamsi();
    public decimal? FinishedPercent { get; set; }
}