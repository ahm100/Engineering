
namespace Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperationDocuments;

public record UpdateDailyProjectOperationDocumentsRequest(
    long Id,
    List<string>? DocumentUrls
    ) : IHttpRequest;
