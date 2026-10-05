namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationById;

public record GetDailyProjectOperationByIdDocumentModel
{
    public long Id { get; set; }
    public string Url { get; set; } = string.Empty;
}