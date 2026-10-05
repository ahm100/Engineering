using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetTotalsByProjectOperationId;

public class GetTotalsByProjectOperationIdQueryHandler : IQueryHandler<GetTotalsByProjectOperationIdQuery, List<ProjectOperationDetail>>
{
    private readonly IProjectOperationDetailRepository _repository;
    private readonly ILogger<GetTotalsByProjectOperationIdQueryHandler> _logger;

    public GetTotalsByProjectOperationIdQueryHandler(ILogger<GetTotalsByProjectOperationIdQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<ProjectOperationDetail>?>> Handle(GetTotalsByProjectOperationIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetTotalsByProjectOperationId(request.ProjectOperationId, ct);

            return result.Any() ? result : Result.Failure<List<ProjectOperationDetail>>(ProjectOperationErrors.ProjectOperationChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperationDetail>>(SharedErrors.UnknownError);
        }
    }
}