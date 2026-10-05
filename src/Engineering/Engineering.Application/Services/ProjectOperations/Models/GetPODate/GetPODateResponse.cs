namespace Engineering.Application.Services.ProjectOperations.Models.GetPODate;

public class GetPODateResponse
{
    public DateTime? PlannedStartDate { get; set; }
    public DateTime? PlannedFinishDate { get; set; }
    public int? PlannedDuration { get; set; }
    public DateTime? ActualStartDate { get; set; }
    public DateTime? ActualFinishDate { get; set; }
    public int? ActualDuration { get; set; }
    public DateTime? BaselineStartDate { get; set; }
    public DateTime? BaselineFinishDate { get; set; }
    public int? BaseLineDuration { get; set; }
}