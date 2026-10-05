using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByCode;

namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByCode;

public class GetCabinTypeByCodeQueryHandler : IQueryHandler<GetCabinTypeByCodeQuery, GetCabinTypeByCodeResponse?>
{
    private readonly ILogger<GetCabinTypeByCodeQueryHandler> _logger;
    private readonly ICabinTypeRepository _repository;

    public GetCabinTypeByCodeQueryHandler(
        ILogger<GetCabinTypeByCodeQueryHandler> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCabinTypeByCodeResponse?>> Handle(GetCabinTypeByCodeQuery request, CT ct)
    {
        try
        {
            var entity = await _repository.GetCabinTypeByCode(
                request.CabinTypeCode,
                request.CompanyId, ct);
            return entity ?? Result.Failure<GetCabinTypeByCodeResponse>(CabinTypeErrors.CabinTypeWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCabinTypeByCodeResponse>(SharedErrors.UnknownError);
        }
    }
}