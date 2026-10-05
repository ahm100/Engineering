using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Application.Services.RequestContractors.Models.GetFilteredRequestContractors;

namespace Engineering.Application.Services.RequestContractors.Queries.GetFilteredRequestContractors;

public class GetFilteredRequestContractorsQueryHandler : IQueryHandler<GetFilteredRequestContractorsQuery, DataResult<List<GetFilteredRequestContractorsModel>>>
{
    private readonly ILogger<GetFilteredRequestContractorsQueryHandler> _logger;
    private readonly IRequestContractorRepository _repository;

    public GetFilteredRequestContractorsQueryHandler(ILogger<GetFilteredRequestContractorsQueryHandler> logger, IRequestContractorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetFilteredRequestContractorsModel>>?>> Handle(GetFilteredRequestContractorsQuery request, CT ct)
    {
        try
        {
            var entities = await _repository.GetFiltered(
                request.Ids,
                request.CostCenterIds,
                request.ProjectIds,
                request.ProjectOperationIds,
                request.ProjectOperationDetailIds,
                request.ServiceInfoIds,
                request.ContractorIds,
                request.Status,
                request.FromDate,
                request.ToDate,
                request.CreatorId,
                request.FilterData,
                request.OrderBy,
                request.CompanyId,
                request.PageIndex,
                request.PageSize,
                ct);

            return entities.Data.Any()
                    ? new DataResult<List<GetFilteredRequestContractorsModel>>
                    {
                        Data = entities.Data,
                        RowCount = entities.RowCount
                    } : Result.Failure<DataResult<List<GetFilteredRequestContractorsModel>>>(RequestContractorErrors.RequestContractorsNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<DataResult<List<GetFilteredRequestContractorsModel>>>(SharedErrors.UnknownError);
        }
    }
}
