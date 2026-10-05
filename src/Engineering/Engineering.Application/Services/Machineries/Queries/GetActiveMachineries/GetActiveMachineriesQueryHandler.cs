
using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetActiveMachineries;

public class GetActiveMachineriesQueryHandler : IQueryHandler<GetActiveMachineriesQuery, DataResult<List<Machinery>>>
{
    private readonly IMachineryRepository _repository;
    private readonly ILogger<GetActiveMachineriesQuery> _logger;

    public GetActiveMachineriesQueryHandler(ILogger<GetActiveMachineriesQuery> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Machinery>>?>> Handle(GetActiveMachineriesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetActiveMachineries(request.FilterData, request.CategoryId, request.MachineryCode, request.MachineryName, request.CompanyId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<Machinery>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<Machinery>>>(MachineryErrors.MachineryWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Machinery>>>(SharedErrors.UnknownError);
        }
    }
}