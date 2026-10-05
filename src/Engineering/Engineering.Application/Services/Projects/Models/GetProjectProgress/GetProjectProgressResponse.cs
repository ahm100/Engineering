namespace Engineering.Application.Services.Projects.Models.GetProjectProgress;

public record GetProjectProgressResponse(
List<GetProjectProgressModel> Data,
int Count);

public class GetProjectProgressModel
{
    public DateTime Date { get; set; }
    public string DateShamsi => Date.ToShamsi();
    public decimal PlannedPercent { get; set; }
    public decimal ActualPercent { get; set; }
}