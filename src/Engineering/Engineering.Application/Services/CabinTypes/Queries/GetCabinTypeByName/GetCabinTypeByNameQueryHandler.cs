using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByName;

namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByName;

public class GetCabinTypeByNameQueryHandler : IQueryHandler<GetCabinTypeByNameQuery, GetCabinTypeByNameResponse?>
{
    private readonly ILogger<GetCabinTypeByNameQueryHandler> _logger;
    private readonly ICabinTypeRepository _repository;

    public GetCabinTypeByNameQueryHandler(
        ILogger<GetCabinTypeByNameQueryHandler> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCabinTypeByNameResponse?>> Handle(GetCabinTypeByNameQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetCabinTypeByName(
                request.CabinTypeName,
                request.CompanyId, ct);
            return entity ?? Result.Failure<GetCabinTypeByNameResponse?>(CabinTypeErrors.CabinTypeWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCabinTypeByNameResponse>(SharedErrors.UnknownError);
        }
    }
}