namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailCommentById;

public record GetProjectOperationDetailCommentDocumentsByIdModel
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
}