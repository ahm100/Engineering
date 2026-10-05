using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperationDetailSchedulings.Queries.GetProjectOperationSchedulings;

public class GetProjectOperationSchedulingsQueryHandler : IQueryHandler<GetProjectOperationSchedulingsQuery, List<ProjectOperation>>
{
    private readonly ILogger<GetProjectOperationSchedulingsQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetProjectOperationSchedulingsQueryHandler(ILogger<GetProjectOperationSchedulingsQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<ProjectOperation>?>> Handle(GetProjectOperationSchedulingsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetForSchecdulingAsync(request.ProjectId, request.OperationInfoId, ct);

            return result.Any() ? result : Result.Failure<List<ProjectOperation>>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperation>>(SharedErrors.UnknownError);
        }
    }
}
