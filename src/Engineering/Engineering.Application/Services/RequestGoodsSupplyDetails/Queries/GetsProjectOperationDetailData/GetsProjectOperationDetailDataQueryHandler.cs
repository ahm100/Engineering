using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsProjectOperationDetailData;

public class GetsProjectOperationDetailDataQueryHandler : IQueryHandler<GetsProjectOperationDetailDataQuery, DataResult<List<ProjectOperationDetail>>>
{
    private readonly ILogger<GetsProjectOperationDetailDataQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetsProjectOperationDetailDataQueryHandler(ILogger<GetsProjectOperationDetailDataQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetail>>?>> Handle(GetsProjectOperationDetailDataQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectOperationDetailData(
                request.ProjectOperationId,
                request.Type,
                request.ProductGroupId,
                request.ProjectOperationDetailId,
                request.FilterData,
                request.ContractorId,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any()
              ? new DataResult<List<ProjectOperationDetail>>
              {
                  Data = result.Data,
                  RowCount = result.RowCount
              }
              : Result.Failure<DataResult<List<ProjectOperationDetail>>>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetail>>>(SharedErrors.UnknownError);
        }
    }
}
