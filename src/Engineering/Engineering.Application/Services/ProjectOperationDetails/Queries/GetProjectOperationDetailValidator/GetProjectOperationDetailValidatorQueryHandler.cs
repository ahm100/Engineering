using Engineering.Application.Abstractions.Data.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailValidator;

public class GetProjectOperationDetailValidatorQueryHandler : IQueryHandler<GetProjectOperationDetailValidatorQuery, bool>
{
    private readonly ILogger<GetProjectOperationDetailValidatorQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailValidatorQueryHandler(ILogger<GetProjectOperationDetailValidatorQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(GetProjectOperationDetailValidatorQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailValidator(request.ProjectOperationId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}
