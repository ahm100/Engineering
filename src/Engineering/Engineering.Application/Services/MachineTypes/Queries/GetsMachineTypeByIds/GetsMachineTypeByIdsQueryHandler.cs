using Engineering.Application.Abstractions.Data.MachineTypes;
using MachineType = Engineering.Domain.Entities.MachineTypes.MachineType;

namespace Engineering.Application.Services.MachineTypes.Queries.GetsMachineTypeByIds;

public class GetsMachineTypeByIdsQueryHandler : IQueryHandler<GetsMachineTypeByIdsQuery, List<MachineType>>
{
    private readonly IMachineTypeRepository _repository;
    private readonly ILogger<GetsMachineTypeByIdsQuery> _logger;

    public GetsMachineTypeByIdsQueryHandler(ILogger<GetsMachineTypeByIdsQuery> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<MachineType>?>> Handle(GetsMachineTypeByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsMachineTypeByIds(request.Items, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<MachineType>>(SharedErrors.UnknownError);
        }
    }
}
