using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.GetCriticalPO;
using Engineering.Application.Services.ProjectOperations.Queries.GetPODate;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetCriticalPO;

public class GetCriticalPOQueryHandler : IQueryHandler<GetCriticalPOQuery, GetCriticalPOResponse?>
{
    private readonly ILogger<GetPODateQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetCriticalPOQueryHandler(ILogger<GetPODateQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCriticalPOResponse?>> Handle(GetCriticalPOQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCriticalPOModel(request.ProjectId,
                request.PageIndex,
                request.PageSize, ct);

            if (result.Data is null || result.RowCount < 1)
                return Result.Failure<GetCriticalPOResponse?>(ProjectOperationErrors.CrititcalPONotFound);
            return new GetCriticalPOResponse(result.Data, result.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCriticalPOResponse>(SharedErrors.UnknownError);
        }
    }
}