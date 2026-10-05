namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistories;

public record GetDailyProjectOperationHistoriesDetailDocumentModel
{
    public long DailyProjectOperationDocumentId { get; set; }
    public string Url { get; set; } = string.Empty;
}
