namespace Engineering.Application.Services.ProjectOperations.Models.GetProjectOperationProgress;

public class GetProjectOperationProgressResponse
{
    public DateTime PlannedStartDate { get; set; }
    public DateTime PlannedFinishDate { get; set; }
    public decimal? PlannedProgressPercent { get; set; }
    public decimal? ActualProgressPercent { get; set; }
}