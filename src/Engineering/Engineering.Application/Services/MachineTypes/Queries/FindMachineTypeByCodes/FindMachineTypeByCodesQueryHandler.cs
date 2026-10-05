using Engineering.Application.Abstractions.Data.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Queries.FindMachineTypeByCodes;

public class FindMachineTypeByCodesQueryHandler : IQueryHandler<FindMachineTypeByCodesQuery, bool>
{
    private readonly ILogger<FindMachineTypeByCodesQueryHandler> _logger;
    private readonly IMachineTypeRepository _repository;

    public FindMachineTypeByCodesQueryHandler(ILogger<FindMachineTypeByCodesQueryHandler> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(FindMachineTypeByCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindMachineTypeByCodes(request.Codes, request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}
