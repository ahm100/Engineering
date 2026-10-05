using Engineering.Application.Abstractions.Data.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailContractors;

public class GetProjectOperationDetailContractorsQueryHandler : IQueryHandler<GetProjectOperationDetailContractorsQuery, DataResult<List<long>>>
{
    private readonly IProjectOperationDetailRepository _repository;
    private readonly ILogger<GetProjectOperationDetailContractorsQueryHandler> _logger;

    public GetProjectOperationDetailContractorsQueryHandler(ILogger<GetProjectOperationDetailContractorsQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetProjectOperationDetailContractorsQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredContractors(request.CostCenterIds,
                request.ProjectIds,
                request.OperationInfoIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds, ct);

            return items.Data.Any() ?
               new DataResult<List<long>>
               {
                   Data = items.Data,
                   RowCount = items.RowCount
               } : Result.Failure<DataResult<List<long>>>(ProjectOperationDetailErrors.ProjectOperationDetailContractorsWithFilterNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
        }
    }
}
