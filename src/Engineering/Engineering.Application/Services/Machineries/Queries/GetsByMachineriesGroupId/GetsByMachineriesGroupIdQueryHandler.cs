
using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Queries.GetsByMachineriesGroupId;

public class GetsByMachineriesGroupIdQueryHandler : IQueryHandler<GetsByMachineriesGroupIdQuery, DataResult<List<Machinery>>>
{
    private readonly IMachineryRepository _repository;
    private readonly ILogger<GetsByMachineriesGroupIdQueryHandler> _logger;

    public GetsByMachineriesGroupIdQueryHandler(ILogger<GetsByMachineriesGroupIdQueryHandler> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Machinery>>?>> Handle(GetsByMachineriesGroupIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByMachineriesGroupId(request.MachineriesGroupId, request.PageIndex, request.PageSize, ct);

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