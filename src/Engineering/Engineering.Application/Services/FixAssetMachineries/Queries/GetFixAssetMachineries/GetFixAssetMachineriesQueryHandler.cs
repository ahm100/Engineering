using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Queries.GetFixAssetMachineries;

public class GetFixAssetMachineriesQueryHandler : IQueryHandler<GetFixAssetMachineriesQuery, DataResult<List<FixAssetMachinery>>>
{
    private readonly IFixAssetMachineryRepository _repository;
    private readonly ILogger<GetFixAssetMachineriesQueryHandler> _logger;

    public GetFixAssetMachineriesQueryHandler(ILogger<GetFixAssetMachineriesQueryHandler> logger, IFixAssetMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<FixAssetMachinery>>?>> Handle(GetFixAssetMachineriesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFixAssetMachineries(
                request.Ids,
                request.MachineryIds,
                request.ContractorIds,
                request.Type,
                request.NumberPlates,
                request.FromDate,
                request.ToDate,
                request.DriverIds,
                request.DriverFilter,
                request.FilterData,
                request.IsActive,
                request.CompanyId,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

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