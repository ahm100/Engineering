using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailDocument;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailDocument;

public class GetsProjectOperationDetailDocumentQueryHandler : IQueryHandler<GetsProjectOperationDetailDocumentQuery, GetsProjectOperationDetailDocumentResponse>
{
    private readonly ILogger<GetsProjectOperationDetailDocumentQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetsProjectOperationDetailDocumentQueryHandler(ILogger<GetsProjectOperationDetailDocumentQueryHandler> logger,
                                                    IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetsProjectOperationDetailDocumentResponse?>> Handle(GetsProjectOperationDetailDocumentQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectOperationDetailDocument(request.ProjectOperationDetailId, ct);

            return result ?? Result.Failure<GetsProjectOperationDetailDocumentResponse>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetsProjectOperationDetailDocumentResponse>(SharedErrors.UnknownError);
        }
    }
}
