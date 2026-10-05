using Engineering.Application.Abstractions.Data.MachineTypes;

namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByNamesOrCodes;

public class GetCabinTypeByNamesOrCodesQueryHandler : IQueryHandler<GetCabinTypeByNamesOrCodesQuery, bool>
{
    private readonly ILogger<GetCabinTypeByNamesOrCodesQueryHandler> _logger;
    private readonly ICabinTypeRepository _repository;

    public GetCabinTypeByNamesOrCodesQueryHandler(
        ILogger<GetCabinTypeByNamesOrCodesQueryHandler> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(GetCabinTypeByNamesOrCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetCabinTypeByNamesOrCodes(
                request.Names,
                request.Codes,
                request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}