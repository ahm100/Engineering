
namespace Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationDocument;

public record GetsDailyProjectOperationDocumentRequest(
    long DailyProjectOperationId
    ) : IHttpRequest;
