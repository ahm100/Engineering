using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Services.CabinTypes.Models.GetsActiveCabinTypes;

namespace Engineering.Application.Services.CabinTypes.Queries.GetsActiveCabinTypes;

public class GetsActiveCabinTypesQueryHandler : IQueryHandler<GetsActiveCabinTypesQuery, DataResult<List<GetsActiveCabinTypeModel>>>
{
    private readonly ICabinTypeRepository _repository;
    private readonly ILogger<GetsActiveCabinTypesQuery> _logger;

    public GetsActiveCabinTypesQueryHandler(
        ILogger<GetsActiveCabinTypesQuery> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsActiveCabinTypeModel>>?>> Handle(GetsActiveCabinTypesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsActiveCabinTypes(
                request.FilterData,
                request.Code,
                request.Name,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsActiveCabinTypeModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsActiveCabinTypeModel>>>(CabinTypeErrors.CabinTypesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsActiveCabinTypeModel>>>(SharedErrors.UnknownError);
        }
    }
}