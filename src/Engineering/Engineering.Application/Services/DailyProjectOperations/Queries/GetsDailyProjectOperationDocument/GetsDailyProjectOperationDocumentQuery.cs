using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationDocument;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyProjectOperationDocument;

public record GetsDailyProjectOperationDocumentQuery(
    long DailyProjectOperationId
    ) : IQuery<GetsDailyProjectOperationDocumentResponse>;