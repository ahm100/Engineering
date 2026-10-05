using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetTotalsByProjectOperationDetailId;
using DailyProjectOperation = Engineering.Domain.Entities.DailyProjectOperations.DailyProjectOperation;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetTotalsByProjectOperationDetailId;

public class GetTotalsByProjectOperationDetailIdQueryHandler : IQueryHandler<GetTotalsByProjectOperationDetailIdQuery, List<DailyProjectOperation>>
{
    private readonly IDailyProjectOperationRepository _repository;
    private readonly ILogger<GetTotalsByProjectOperationDetailIdQueryHandler> _logger;

    public GetTotalsByProjectOperationDetailIdQueryHandler(ILogger<GetTotalsByProjectOperationDetailIdQueryHandler> logger, IDailyProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<DailyProjectOperation>?>> Handle(GetTotalsByProjectOperationDetailIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetTotalsByProjectOperationDetailId(request.ProjectOperationDetailId, ct);

            return result.Any() ? result : Result.Failure<List<DailyProjectOperation>>(ProjectOperationErrors.ProjectOperationChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<DailyProjectOperation>>(SharedErrors.UnknownError);
        }
    }
}