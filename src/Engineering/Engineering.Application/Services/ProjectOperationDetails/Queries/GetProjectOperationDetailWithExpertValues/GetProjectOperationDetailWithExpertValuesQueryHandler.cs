using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithExpertValues;

public class GetProjectOperationDetailWithExpertValuesQueryHandler : IQueryHandler<GetProjectOperationDetailWithExpertValuesQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetProjectOperationDetailWithExpertValuesQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailWithExpertValuesQueryHandler(ILogger<GetProjectOperationDetailWithExpertValuesQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetProjectOperationDetailWithExpertValuesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailWithExpertValues(request.Id, ct);
            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
