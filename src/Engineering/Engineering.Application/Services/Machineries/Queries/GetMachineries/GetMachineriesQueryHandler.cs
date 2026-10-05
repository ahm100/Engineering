using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetMachineries;

public class GetMachineriesQueryHandler : IQueryHandler<GetMachineriesQuery, DataResult<List<Machinery>>>
{
    private readonly IMachineryRepository _repository;
    private readonly ILogger<GetMachineriesQueryHandler> _logger;

    public GetMachineriesQueryHandler(ILogger<GetMachineriesQueryHandler> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Machinery>>?>> Handle(GetMachineriesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetMachineries(request.Ids, request.FilterData, request.CategoryId, request.MachineryCode, request.MachineryName, request.IsActive, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<Machinery>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<Machinery>>>(MachineryErrors.FilteredMachineryNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Machinery>>>(SharedErrors.UnknownError);
        }
    }
}