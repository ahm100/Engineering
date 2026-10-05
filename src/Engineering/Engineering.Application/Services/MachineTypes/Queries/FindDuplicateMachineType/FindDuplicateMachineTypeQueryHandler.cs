using Engineering.Application.Abstractions.Data.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Queries.FindDuplicateMachineType;

public class FindDuplicateMachineTypeQueryHandler : IQueryHandler<FindDuplicateMachineTypeQuery, bool>
{
    private readonly ILogger<FindDuplicateMachineTypeQueryHandler> _logger;
    private readonly IMachineTypeRepository _repository;

    public FindDuplicateMachineTypeQueryHandler(ILogger<FindDuplicateMachineTypeQueryHandler> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(FindDuplicateMachineTypeQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindDuplicateMachineType(request.Names, request.Codes, request.FromWeights, request.UntilWeights, request.CabinTypCodes, request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}
