using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithStandards;

public class GetProjectOperationDetailWithStandardsQueryHandler : IQueryHandler<GetProjectOperationDetailWithStandardsQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetProjectOperationDetailWithStandardsQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailWithStandardsQueryHandler(ILogger<GetProjectOperationDetailWithStandardsQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetProjectOperationDetailWithStandardsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailWithStandards(request.Id, ct);
            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
