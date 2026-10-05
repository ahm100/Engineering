using Engineering.Application.Abstractions.Data.Machineries;

namespace Engineering.Application.Services.MachineriesGroups.Queries.FindMachineriesGroupByNamesOrCodes;

public class FindMachineriesGroupByNamesOrCodesQueryHandler : IQueryHandler<FindMachineriesGroupByNamesOrCodesQuery, bool>
{
    private readonly ILogger<FindMachineriesGroupByNamesOrCodesQueryHandler> _logger;
    private readonly IMachineriesGroupRepository _repository;

    public FindMachineriesGroupByNamesOrCodesQueryHandler(ILogger<FindMachineriesGroupByNamesOrCodesQueryHandler> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(FindMachineriesGroupByNamesOrCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindMachineriesGroupByNamesOrCodes(request.Names, request.Codes, request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}
