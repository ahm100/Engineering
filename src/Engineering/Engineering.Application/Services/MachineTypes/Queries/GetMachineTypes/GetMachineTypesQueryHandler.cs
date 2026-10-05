using Engineering.Application.Abstractions.Data.MachineTypes;
using MachineType = Engineering.Domain.Entities.MachineTypes.MachineType;
namespace Engineering.Application.Services.MachineTypes.Queries.GetMachineTypes;

public class GetMachineTypesQueryHandler : IQueryHandler<GetMachineTypesQuery, DataResult<List<MachineType>>>
{
    private readonly IMachineTypeRepository _repository;
    private readonly ILogger<GetMachineTypesQueryHandler> _logger;

    public GetMachineTypesQueryHandler(ILogger<GetMachineTypesQueryHandler> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<MachineType>>?>> Handle(GetMachineTypesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetMachineTypes(request.Ids, request.FilterData, request.IsActive, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

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