using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsProjectOperationDetailByContractorIds;

public class GetsProjectOperationDetailByContractorIdsQueryHandler : IQueryHandler<GetsProjectOperationDetailByContractorIdsQuery, DataResult<List<ProjectOperationDetail>>>
{
    private readonly IProjectOperationDetailRepository _repository;
    private readonly ILogger<GetsProjectOperationDetailByContractorIdsQueryHandler> _logger;

    public GetsProjectOperationDetailByContractorIdsQueryHandler(ILogger<GetsProjectOperationDetailByContractorIdsQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetail>>?>> Handle(GetsProjectOperationDetailByContractorIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectOperationDetailByContractorIds(
                request.CostCenterIds,
                request.ProjectIds,
                request.OperationInfoIds,
                request.ProjectOperationIds,
                request.ContractorIds,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperationDetail>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperationDetail>>>(ProjectOperationDetailErrors.ProjectOperationDetailWithFilterNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetail>>>(SharedErrors.UnknownError);
        }
    }
}