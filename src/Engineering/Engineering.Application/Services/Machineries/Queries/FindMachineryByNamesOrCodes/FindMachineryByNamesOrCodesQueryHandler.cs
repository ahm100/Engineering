using Engineering.Application.Abstractions.Data.Machineries;

namespace Engineering.Application.Services.Machineries.Queries.FindMachineryByNamesOrCodes;

public class FindMachineryByNamesOrCodesQueryHandler : IQueryHandler<FindMachineryByNamesOrCodesQuery, bool>
{
    private readonly ILogger<FindMachineryByNamesOrCodesQueryHandler> _logger;
    private readonly IMachineryRepository _repository;

    public FindMachineryByNamesOrCodesQueryHandler(ILogger<FindMachineryByNamesOrCodesQueryHandler> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(FindMachineryByNamesOrCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindMachineryByNamesOrCodes(request.Names, request.Codes, request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}
