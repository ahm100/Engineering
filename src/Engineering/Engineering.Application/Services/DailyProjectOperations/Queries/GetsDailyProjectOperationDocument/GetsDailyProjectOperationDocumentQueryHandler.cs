using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationDocument;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyProjectOperationDocument;

public class GetsDailyProjectOperationDocumentQueryHandler : IQueryHandler<GetsDailyProjectOperationDocumentQuery, GetsDailyProjectOperationDocumentResponse>
{
    private readonly ILogger<GetsDailyProjectOperationDocumentQueryHandler> _logger;
    private readonly IDailyProjectOperationRepository _repository;

    public GetsDailyProjectOperationDocumentQueryHandler(ILogger<GetsDailyProjectOperationDocumentQueryHandler> logger,
                                                    IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetsDailyProjectOperationDocumentResponse?>> Handle(GetsDailyProjectOperationDocumentQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsDailyProjectOperationDocument(request.DailyProjectOperationId, ct);

            return result ?? Result.Failure<GetsDailyProjectOperationDocumentResponse>(DailyProjectOperationErrors.DailyProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetsDailyProjectOperationDocumentResponse>(SharedErrors.UnknownError);
        }
    }
}
