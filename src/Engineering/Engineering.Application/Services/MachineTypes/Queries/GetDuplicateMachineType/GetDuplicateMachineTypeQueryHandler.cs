using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Domain.Entities.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Queries.GetDuplicateMachineType;

public class GetDuplicateMachineTypeQueryHandler : IQueryHandler<GetDuplicateMachineTypeQuery, MachineType?>
{
    private readonly ILogger<GetDuplicateMachineTypeQueryHandler> _logger;
    private readonly IMachineTypeRepository _repository;

    public GetDuplicateMachineTypeQueryHandler(ILogger<GetDuplicateMachineTypeQueryHandler> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineType?>> Handle(GetDuplicateMachineTypeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetDuplicateMachineType(request.MachineTypeTitle, request.FromWeight, request.UntilWeight, request.CabinTypeCode, request.CompanyId, ct);

            return result ?? Result.Failure<MachineType?>(MachineTypeErrors.MachineTypeIsDuplicate);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineType?>(SharedErrors.UnknownError);
        }
    }
}