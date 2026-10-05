using Engineering.Application.Abstractions.Data.MachineTypes;
using CabinType = Engineering.Domain.Entities.MachineTypes.CabinType;

namespace Engineering.Application.Services.CabinTypes.Queries.GetsCabinTypeByIds;

public class GetsCabinTypeByIdsQueryHandler : IQueryHandler<GetsCabinTypeByIdsQuery, List<CabinType>>
{
    private readonly ICabinTypeRepository _repository;
    private readonly ILogger<GetsCabinTypeByIdsQuery> _logger;

    public GetsCabinTypeByIdsQueryHandler(
        ILogger<GetsCabinTypeByIdsQuery> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<CabinType>?>> Handle(GetsCabinTypeByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsCabinTypeByIds(request.Items, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<CabinType>>(SharedErrors.UnknownError);
        }
    }
}