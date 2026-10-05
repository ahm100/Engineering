using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetsMachineryByIds;

public class GetsMachineryByIdsQueryHandler : IQueryHandler<GetsMachineryByIdsQuery, DataResult<List<Machinery?>>>
{
    private readonly IMachineryRepository _repository;
    private readonly ILogger<GetsMachineryByIdsQueryHandler> _logger;

    public GetsMachineryByIdsQueryHandler(ILogger<GetsMachineryByIdsQueryHandler> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Machinery?>>?>> Handle(GetsMachineryByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsMachineryByIds(request.ids, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<Machinery?>>
                {
                    Data = result.Data!,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<Machinery?>>>(MachineryErrors.FilteredMachineryNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Machinery?>>>(SharedErrors.UnknownError);
        }
    }
}