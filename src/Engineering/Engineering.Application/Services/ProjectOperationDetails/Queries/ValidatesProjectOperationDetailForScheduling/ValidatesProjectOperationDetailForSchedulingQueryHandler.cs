using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.ValidatesProjectOperationDetailForScheduling;

public class ValidatesProjectOperationDetailForSchedulingQueryHandler : IQueryHandler<ValidatesProjectOperationDetailForSchedulingQuery, DataResult<List<ProjectOperationDetail>>>
{
    private readonly IProjectOperationDetailRepository _repository;
    private readonly ILogger<ValidatesProjectOperationDetailForSchedulingQueryHandler> _logger;

    public ValidatesProjectOperationDetailForSchedulingQueryHandler(ILogger<ValidatesProjectOperationDetailForSchedulingQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetail>>?>> Handle(ValidatesProjectOperationDetailForSchedulingQuery request, CT ct)
    {
        try
        {
            var result = await _repository.ValidatesProjectOperationDetailForScheduling(request.OperationInfoIds, request.OperationLocationIds, ct);

            return result.Any() ?
                new DataResult<List<ProjectOperationDetail>>
                {
                    Data = result,
                } : Result.Failure<DataResult<List<ProjectOperationDetail>>>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetail>>>(SharedErrors.UnknownError);
        }
    }
}
