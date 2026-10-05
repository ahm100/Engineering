using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Queries.GetsActiveMachineType;

public class GetsActiveMachineTypeQueryHandler : IQueryHandler<GetsActiveMachineTypeQuery, DataResult<List<MachineType>>>
{
    private readonly IMachineTypeRepository _repository;
    private readonly ILogger<GetsActiveMachineTypeQuery> _logger;

    public GetsActiveMachineTypeQueryHandler(ILogger<GetsActiveMachineTypeQuery> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<MachineType>>?>> Handle(GetsActiveMachineTypeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsActiveMachineType(request.FilterData, request.CompanyId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<MachineType>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<MachineType>>>(MachineTypeErrors.FilteredMachinTypeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<MachineType>>>(SharedErrors.UnknownError);
        }
    }
}