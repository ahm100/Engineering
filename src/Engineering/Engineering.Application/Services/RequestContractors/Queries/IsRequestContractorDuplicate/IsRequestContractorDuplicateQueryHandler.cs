using Engineering.Application.Abstractions.Data.RequestContractors;

namespace Engineering.Application.Services.RequestContractors.Queries.IsRequestContractorDuplicate;

public class IsRequestContractorDuplicateQueryHandler : IQueryHandler<IsRequestContractorDuplicateQuery, bool>
{
    private readonly ILogger<IsRequestContractorDuplicateQueryHandler> _logger;
    private readonly IRequestContractorRepository _repository;

    public IsRequestContractorDuplicateQueryHandler(ILogger<IsRequestContractorDuplicateQueryHandler> logger, IRequestContractorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(IsRequestContractorDuplicateQuery request, CT ct)
    {
        try
        {
            var result = await _repository.IsRequestContractorDuplicate(
                request.ProjectOperationDetailId,
                request.ServiceInfoId,
                ct);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}
