using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperations.Models.GetFltrProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetFltrProjectOperation;

public class GetFltrProjectOperationQueryHandler : IQueryHandler<GetFltrProjectOperationQuery, List<GetFltrProjectOperationModel>>
{
    private readonly ILogger<GetFltrProjectOperationQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetFltrProjectOperationQueryHandler(ILogger<GetFltrProjectOperationQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<GetFltrProjectOperationModel>?>> Handle(GetFltrProjectOperationQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFltrProjectOperation(request.CostCenterId,
                request.ProjectIds,
                request.OperationInfoIds,
                request.MeasureUnitIds,
                request.MinPrice,
                request.MaxPrice,
                request.FilterData,
                request.PageIndex,
                request.PageSize
                , ct);
            return result ?? Result.Failure<List<GetFltrProjectOperationModel>>(ProjectOperationErrors.ProjectOperationWithIdNotFound)!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<GetFltrProjectOperationModel>>(SharedErrors.UnknownError)!;
        }
    }
}