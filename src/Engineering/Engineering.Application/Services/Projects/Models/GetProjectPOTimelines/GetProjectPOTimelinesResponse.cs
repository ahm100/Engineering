namespace Engineering.Application.Services.Projects.Models.GetProjectPOTimelines;

public record GetProjectPOTimelinesResponse(
List<GetProjectPOTimelinesModel> Data,
int Count);

public class GetProjectPOTimelinesModel
{
    public long Id { get; set; }
    public decimal WorkLoad { get; set; }
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public decimal PlannedStartProgressPercent { get; set; }
    public decimal PlannedEndProgressPercent { get; set; }
    public DateTime? PlannedStartDate { get; set; }
    public string? PlannedStartDateShamsi => PlannedStartDate.ToShamsi();
    public DateTime? PlannedFinishDate { get; set; }
    public string? PlannedFinishDateShamsi => PlannedFinishDate.ToShamsi();
    public decimal ActualStartProgressPercent { get; set; }
    public decimal ActualEndProgressPercent { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public string? ActualStartDateShamsi => ActualStartDate.ToShamsi();
    public DateTime? ActualFinishDate { get; set; }
    public string? ActualFinishDateShamsi => ActualFinishDate.ToShamsi();
    public decimal? PlannedStartProjectProgressPercent { get; set; }
    public decimal? PlannedEndProjectProgressPercent { get; set; }
    public decimal? ActualStartProjectProgressPercent { get; set; }
    public decimal? ActualEndProjectProgressPercent { get; set; }
}