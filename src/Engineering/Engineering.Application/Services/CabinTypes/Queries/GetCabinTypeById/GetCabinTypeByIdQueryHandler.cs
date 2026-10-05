using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeById;

namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeById;

public class GetCabinTypeByIdQueryHandler : IQueryHandler<GetCabinTypeByIdQuery, GetCabinTypeByIdResponse>
{
    private readonly ILogger<GetCabinTypeByIdQueryHandler> _logger;
    private readonly ICabinTypeRepository _repository;

    public GetCabinTypeByIdQueryHandler(
        ILogger<GetCabinTypeByIdQueryHandler> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCabinTypeByIdResponse?>> Handle(GetCabinTypeByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCabinTypeByIdForResponse(request.Id, ct);
            return result ?? Result.Failure<GetCabinTypeByIdResponse>(CabinTypeErrors.CabinTypeWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCabinTypeByIdResponse>(SharedErrors.UnknownError);
        }
    }
}