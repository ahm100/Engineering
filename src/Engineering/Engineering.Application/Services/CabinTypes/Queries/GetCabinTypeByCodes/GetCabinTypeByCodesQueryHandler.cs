using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByCodes;

public class GetCabinTypeByCodesQueryHandler : IQueryHandler<GetCabinTypeByCodesQuery, List<CabinType>?>
{
    private readonly ILogger<GetCabinTypeByCodesQueryHandler> _logger;
    private readonly ICabinTypeRepository _repository;

    public GetCabinTypeByCodesQueryHandler(
        ILogger<GetCabinTypeByCodesQueryHandler> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<CabinType>?>> Handle(GetCabinTypeByCodesQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetCabinTypeByCodes(
                request.CabinTypeCodes,
                request.CompanyId, ct);
            return entity ?? Result.Failure<List<CabinType>>(CabinTypeErrors.CabinTypeWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<CabinType>>(SharedErrors.UnknownError);
        }
    }
}