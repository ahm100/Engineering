
using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetActiveFixAssetMachineries;

public class GetActiveFixAssetMachineriesQueryHandler : IQueryHandler<GetActiveFixAssetMachineriesQuery, DataResult<List<FixAssetMachinery>>>
{
    private readonly IFixAssetMachineryRepository _repository;
    private readonly ILogger<GetActiveFixAssetMachineriesQuery> _logger;

    public GetActiveFixAssetMachineriesQueryHandler(ILogger<GetActiveFixAssetMachineriesQuery> logger, IFixAssetMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<FixAssetMachinery>>?>> Handle(GetActiveFixAssetMachineriesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetActiveFixAssetMachineries(request.MachineryIds, request.Type, request.NumberPlates,
                request.FilterData, request.CompanyId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<FixAssetMachinery>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<FixAssetMachinery>>>(FixAssetMachineryErrors.FilteredFixAssetMachineryNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<FixAssetMachinery>>>(SharedErrors.UnknownError);
        }
    }
}