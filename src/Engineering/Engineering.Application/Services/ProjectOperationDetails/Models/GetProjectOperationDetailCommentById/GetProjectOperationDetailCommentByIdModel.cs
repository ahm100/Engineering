namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailCommentById;

public record GetProjectOperationDetailCommentByIdModel
{
    public long Id { get; set; }
    public string Comment { get; set; } = string.Empty;
    public long? ParentId { get; set; }
    public int DayFromNow => (int)Math.Floor(DateTime.Now.Subtract(this.Created).TotalDays);
    public DateTime Created { get; set; }
    public string? Creator { get; set; }
    public bool HasChild { get; set; }
    public List<GetProjectOperationDetailCommentDocumentsByIdModel> Documents { get; set; } = new();
}
