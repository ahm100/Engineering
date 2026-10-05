using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsServiceByProjectOperationDetailIds;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsServiceByProjectOperationDetailIds;

public class GetsServiceByProjectOperationDetailIdsQueryHandler : IQueryHandler<GetsServiceByProjectOperationDetailIdsQuery, DataResult<List<GetsServiceByProjectOperationDetailIdsModel>>>
{
    private readonly IProjectOperationDetailRepository _repository;
    private readonly ILogger<GetsServiceByProjectOperationDetailIdsQueryHandler> _logger;

    public GetsServiceByProjectOperationDetailIdsQueryHandler(ILogger<GetsServiceByProjectOperationDetailIdsQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsServiceByProjectOperationDetailIdsModel>>?>> Handle(GetsServiceByProjectOperationDetailIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsServiceByProjectOperationDetailIds(
                request.ProjectOperationDetailIds,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsServiceByProjectOperationDetailIdsModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsServiceByProjectOperationDetailIdsModel>>>(ContractorServiceErrors.NotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsServiceByProjectOperationDetailIdsModel>>>(SharedErrors.UnknownError);
        }
    }
}